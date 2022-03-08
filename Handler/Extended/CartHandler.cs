using Microsoft.Data.Sqlite;
using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;
using Shopping_API.Models;
using Shopping_API.Utils;

namespace Shopping_API.Handler.Default
{
    /// <summary>
    ///     Defines the corresponding handler for carts.
    /// </summary>
    public class CartHandler : BaseHandler<Cart>
    {
        /// <summary>
        ///     Lists all the carts in the database.
        /// </summary>
        /// 
        /// <returns>
        ///     A response with a list containing all the brands from the database.
        /// </returns>
        public Response<Cart> List()
        {
            try
            {
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);

                Result.Data = cartEntity.Select();

                foreach (Cart cart in Result.Data)
                {
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCarts = productCartEntity.Select();
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            return Result;
        }

        /// <summary>
        ///     Returns a specific cart from the database.
        /// </summary>
        /// 
        /// <param name="_cartId">The ID of a cart.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database (if it exists).
        /// </returns>
        public Response<Cart> Get(int _cartId)
        {
            List<Cart> carts = new();

            try
            {
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.Id = _cartId;

                Result.Data = cartEntity.Select();

                foreach (Cart cart in Result.Data)
                {
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCarts = productCartEntity.Select();
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = carts;

            return Result;
        }

        /// <summary>
        ///     Returns a specific cart from the database. This method differs from the
        /// <see cref="Get(int)"/> method because this one finds a cart based on a user ID.
        /// This method also creates a new cart for the given user if no cart was originally
        /// found.
        /// </summary>
        /// 
        /// <param name="_userId">The ID of a user.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database (new or existing).
        /// </returns>
        public Response<Cart> GetFromUser(int _userId)
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        //  First, searches the cart.
                        CartEntity cartEntity = new();
                        cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                        cartEntity.Filters.UserId = _userId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.SQLJoin();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //  If the cart exists, loads it...
                                Cart cart = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Subtotal = reader.GetDecimal(1),
                                    Discount = reader.GetDecimal(2),
                                    Shipping = reader.GetDecimal(3),
                                    Total = reader.GetDecimal(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    User = new()
                                    {
                                        Id = reader.GetInt32(8),
                                        Username = reader.GetString(9),
                                        Name = reader.GetString(10)
                                    },
                                    Coupon = !reader.IsDBNull(11) ? new()
                                    {
                                        Id = reader.GetInt32(11),
                                        Code = reader.GetString(12),
                                        Description = reader.GetString(13),
                                        Discount = reader.GetDecimal(14)
                                    } : null,
                                    ProductCarts = new()
                                };

                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        cart.ProductCarts.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6),
                                            Product = new()
                                            {
                                                Id = subReader.GetInt32(7),
                                                Code = subReader.GetString(8),
                                                Name = subReader.GetString(9),
                                                Price = subReader.GetDecimal(10),
                                                Stock = subReader.GetInt32(11)
                                            }
                                        });
                                    }
                                }

                                carts.Add(cart);                                
                            }
                            else
                            {
                                //  ...else, creates a new cart for the user.
                                cartEntity.Values.UserId = _userId;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLInsert();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to get the cart from the given user.",
                                            new Exception("Cannot create a new cart for the user.")
                                        );
                                }

                                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                                cartEntity.Filters.UserId = _userId;

                                subCommand = db.CreateCommand();
                                subCommand.CommandText = cartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        Cart cart = new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Subtotal = subReader.GetDecimal(1),
                                            Discount = subReader.GetDecimal(2),
                                            Shipping = subReader.GetDecimal(3),
                                            Total = subReader.GetDecimal(4),
                                            CreatedAt = subReader.GetDateTime(5),
                                            User = new()
                                            {
                                                Id = subReader.GetInt32(8),
                                                Username = subReader.GetString(9),
                                                Name = subReader.GetString(10)
                                            },
                                            ProductCarts = new()
                                        };

                                        carts.Add(cart);
                                    }
                                }
                            }
                        }

                        transaction.Commit();
                    }                    
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = carts;

            return Result;
        }

        /// <summary>
        ///     Adds a product to a cart. The product must exists in the database, and it
        /// must not be added previously to the same cart.
        /// </summary>
        /// 
        /// <param name="_cartId">The ID of the cart.</param>
        /// <param name="_productId">The ID of the product.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database (new or existing).
        /// </returns>
        public Response<Cart> AddProduct(int _cartId, int _productId)
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        //  First, searches the cart.
                        CartEntity cartEntity = new();
                        cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.SQLJoin();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //  If the cart exists, loads the cart...
                                Cart cart = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Subtotal = reader.GetDecimal(1),
                                    Discount = reader.GetDecimal(2),
                                    Shipping = reader.GetDecimal(3),
                                    Total = reader.GetDecimal(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    User = new()
                                    {
                                        Id = reader.GetInt32(8),
                                        Username = reader.GetString(9),
                                        Name = reader.GetString(10)
                                    },
                                    Coupon = !reader.IsDBNull(11) ? new()
                                    {
                                        Id = reader.GetInt32(11),
                                        Code = reader.GetString(12),
                                        Description = reader.GetString(13),
                                        Discount = reader.GetDecimal(14)
                                    } : null,
                                    ProductCarts = new()
                                };

                                //  ...verifies if the product has already been added...
                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Filters.CartId = _cartId;
                                productCartEntity.Filters.ProductId = _productId;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLSelect();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    if (subReader.HasRows)
                                    {
                                        transaction.Rollback();

                                        throw new Exception(
                                                "Failed to add the product to the cart.",
                                                new Exception(
                                                    "The product already exists in the cart. " +
                                                    "Consider change its quantity instead."
                                                )
                                            );
                                    }
                                }

                                //  ...finds the added product...
                                Product product = new();

                                ProductEntity productEntity = new();
                                productEntity.Filters.Id = _productId;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productEntity.SQLSelect();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    if (subReader.Read()) 
                                    {
                                        product.Id = subReader.GetInt32(0);
                                        product.Code = subReader.GetString(1);
                                        product.Name = subReader.GetString(2);
                                        product.Price = subReader.GetDecimal(3);
                                        product.Stock = subReader.GetInt32(4);
                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        throw new Exception(
                                                "Failed to add the product to the cart.",
                                                new Exception("Cannot find the given product to add.")
                                            );
                                    }
                                }

                                if (product.Stock <= 0)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("There is no stock available for the given product.")
                                        );
                                }

                                //  ...adds the product to the cart...
                                productCartEntity.Values.Price = product.Price;
                                productCartEntity.Values.Quantity = 1;
                                productCartEntity.Values.Total = product.Price;
                                productCartEntity.Values.CartId = cart.Id;
                                productCartEntity.Values.ProductId = product.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLInsert();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot bind a product to the user cart.")
                                        );
                                }

                                //  ...updates the product stock...
                                product.Stock--;

                                productEntity.Filters.Id = product.Id;
                                productEntity.Values.Stock = product.Stock;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot update the product stock.")
                                        );
                                }

                                //  ...gets the (updated) list of the products on the cart...
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        cart.ProductCarts.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6),
                                            Product = new()
                                            {
                                                Id = subReader.GetInt32(7),
                                                Code = subReader.GetString(8),
                                                Name = subReader.GetString(9),
                                                Price = subReader.GetDecimal(10),
                                                Stock = subReader.GetInt32(11)
                                            }
                                        });
                                    }
                                }

                                //  ...updates the cart values...
                                decimal subtotal = EstimateSubtotal(cart);
                                decimal shipping = EstimateShipping(cart);
                                decimal discount = EstimateDiscount(cart);
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot update the cart values.")
                                        );
                                }

                                cart.Subtotal = subtotal;
                                cart.Shipping = shipping;
                                cart.Discount = discount;
                                cart.Total = total;

                                //  ...and, finally, adds the resulted cart to the list.
                                carts.Add(cart);                                
                            }
                            else
                            {
                                //  ...else, throw an exception.
                                transaction.Rollback();

                                throw new Exception(
                                        "Failed to add the product to the cart.",
                                        new Exception("The given cart does not exists.")
                                    );
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = carts;

            return Result;
        }

        /// <summary>
        ///     Changes the quantity of a product in a cart. The quantity must not be zero, 
        /// negative, or greater than the original stock of the product.
        /// </summary>
        /// 
        /// <param name="_cartId">The ID of the cart.</param>
        /// <param name="_productId">The ID of the product.</param>
        /// <param name="_quantity">The new quantity for the product.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        public Response<Cart> ChangeQuantity(int _cartId, int _productId, int _quantity)
        {
            List<Cart> carts = new();

            try
            {
                //  Before anything, validates the quantity input.
                if (_quantity == 0)
                {
                    throw new Exception(
                            "Failed to change the quantity of a product in the cart.",
                            new Exception("Cannot change the quantity to zero. Consider removing the product instead.")
                        );
                }
                else if (_quantity < 0)
                {
                    throw new Exception(
                            "Failed to change the quantity of a product in the cart.",
                            new Exception("The quantity must be non-negative.")
                        );
                }

                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        //  First, searches the cart.
                        CartEntity cartEntity = new();
                        cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.SQLJoin();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //  If the cart exists, loads the cart...
                                Cart cart = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Subtotal = reader.GetDecimal(1),
                                    Discount = reader.GetDecimal(2),
                                    Shipping = reader.GetDecimal(3),
                                    Total = reader.GetDecimal(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    User = new()
                                    {
                                        Id = reader.GetInt32(8),
                                        Username = reader.GetString(9),
                                        Name = reader.GetString(10)
                                    },
                                    Coupon = !reader.IsDBNull(11) ? new()
                                    {
                                        Id = reader.GetInt32(11),
                                        Code = reader.GetString(12),
                                        Description = reader.GetString(13),
                                        Discount = reader.GetDecimal(14)
                                    } : null,
                                    ProductCarts = new()
                                };

                                //  ...finds all the products from the cart...
                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        cart.ProductCarts.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6),
                                            Product = new()
                                            {
                                                Id = subReader.GetInt32(7),
                                                Code = subReader.GetString(8),
                                                Name = subReader.GetString(9),
                                                Price = subReader.GetDecimal(10),
                                                Stock = subReader.GetInt32(11)
                                            }
                                        });
                                    }
                                }

                                //  ...verifies if the product exists on cart...
                                ProductCart? productCart = cart.ProductCarts.FirstOrDefault(p => p.ProductId == _productId);

                                if (productCart == null)
                                {
                                    throw new Exception(
                                            "Failed to change the quantity of a product in the cart.",
                                            new Exception("The given product does not exists inside the given cart.")
                                        );
                                }

                                //  ...verifies if it is possible to change the quantity to the given value...
                                int fullStock = 0;

                                if (productCart.Product != null)
                                {
                                    fullStock = productCart.Quantity + productCart.Product.Stock;

                                    if (fullStock < _quantity)
                                    {
                                        throw new Exception(
                                                "Failed to change the quantity of a product in the cart.",
                                                new Exception("There is not enough stock to update the product quantity.")
                                            );
                                    }
                                }
                                else
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to change the quantity of a product in the cart.",
                                            new Exception("There was a problem finding the producs on the cart.")
                                        );
                                }


                                //  ...updates the product stock...
                                ProductEntity productEntity = new();
                                productEntity.Filters.Id = productCart.ProductId;
                                productEntity.Values.Stock = fullStock - _quantity;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to change the quantity of a product in the cart.",
                                            new Exception("Cannot update the product stock.")
                                        );
                                }

                                //  ...updates the product quantity...
                                productCartEntity.Filters.Id = productCart.Id;
                                productCartEntity.Values.Quantity = _quantity;
                                productCartEntity.Values.Total = Math.Round(productCart.Price * _quantity, 2);

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to change the quantity of a product in the cart.",
                                            new Exception("Cannot update the product quantity.")
                                        );
                                }

                                //  ...gets the (updated) list of the products on the cart...
                                cart.ProductCarts.Clear();

                                productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        cart.ProductCarts.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6),
                                            Product = new()
                                            {
                                                Id = subReader.GetInt32(7),
                                                Code = subReader.GetString(8),
                                                Name = subReader.GetString(9),
                                                Price = subReader.GetDecimal(10),
                                                Stock = subReader.GetInt32(11)
                                            }
                                        });
                                    }
                                }

                                //  ...updates the cart values...
                                decimal subtotal = EstimateSubtotal(cart);
                                decimal shipping = EstimateShipping(cart);
                                decimal discount = EstimateDiscount(cart);
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot update the cart values.")
                                        );
                                }

                                cart.Subtotal = subtotal;
                                cart.Shipping = shipping;
                                cart.Discount = discount;
                                cart.Total = total;

                                //  ...and, finally, adds the resulted cart to the list.
                                carts.Add(cart);
                            }
                            else
                            {
                                //  ...else, throw an exception.
                                transaction.Rollback();

                                throw new Exception(
                                        "Failed to remove all products from the cart.",
                                        new Exception("The given cart does not exists.")
                                    );
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = carts;

            return Result;
        }

        /// <summary>
        ///     Removes a product to a cart. The product must exists in the database, and it
        /// must be added to the cart before.
        /// </summary>
        /// 
        /// <param name="_cartId">The ID of the cart.</param>
        /// <param name="_productId">The ID of the product.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        public Response<Cart> RemoveProduct(int _cartId, int _productId)
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        //  First, searches the cart.
                        CartEntity cartEntity = new();
                        cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.SQLJoin();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //  If the cart exists, loads the cart...
                                Cart cart = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Subtotal = reader.GetDecimal(1),
                                    Discount = reader.GetDecimal(2),
                                    Shipping = reader.GetDecimal(3),
                                    Total = reader.GetDecimal(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    User = new()
                                    {
                                        Id = reader.GetInt32(8),
                                        Username = reader.GetString(9),
                                        Name = reader.GetString(10)
                                    },
                                    Coupon = !reader.IsDBNull(11) ? new()
                                    {
                                        Id = reader.GetInt32(11),
                                        Code = reader.GetString(12),
                                        Description = reader.GetString(13),
                                        Discount = reader.GetDecimal(14)
                                    } : null,
                                    ProductCarts = new()
                                };

                                //  ...loads the products in the cart...
                                List<ProductCart> productsCart = new();

                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Filters.CartId = cart.Id;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLSelect();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    if (!subReader.HasRows)
                                    {
                                        transaction.Rollback();

                                        throw new Exception(
                                                "Failed to remove the product to the cart.",
                                                new Exception("The cart is already empty.")
                                            );
                                    }

                                    while (subReader.Read())
                                    {
                                        productsCart.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6)
                                        });
                                    }
                                }

                                //  ...verifies if the product exists on cart...
                                ProductCart? productCart = productsCart.FirstOrDefault(p => p.ProductId == _productId);

                                if (productCart == null)
                                {
                                    throw new Exception(
                                            "Failed to remove the product to the cart.",
                                            new Exception("The given product does not exists inside the given cart.")
                                        );
                                }

                                //  ...removes the product from the cart...
                                productCartEntity = new();
                                productCartEntity.Filters.Id = productCart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLDelete();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to remove the product to the cart.",
                                            new Exception("Cannot remove the given product from the user cart.")
                                        );
                                }

                                //  ...finds the removed product...
                                Product product = new();

                                ProductEntity productEntity = new();
                                productEntity.Filters.Id = productCart.ProductId;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productEntity.SQLSelect();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    if (subReader.Read())
                                    {
                                        product.Id = subReader.GetInt32(0);
                                        product.Code = subReader.GetString(1);
                                        product.Name = subReader.GetString(2);
                                        product.Price = subReader.GetDecimal(3);
                                        product.Stock = subReader.GetInt32(4);
                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        throw new Exception(
                                                "Failed to add the product to the cart.",
                                                new Exception("Cannot find the given product to add.")
                                            );
                                    }
                                }

                                //  ...updates the product stock...
                                product.Stock++;

                                productEntity = new();
                                productEntity.Filters.Id = product.Id;
                                productEntity.Values.Stock = product.Stock;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot update the product stock.")
                                        );
                                }

                                //  ...gets the (updated) list of the products on the cart...
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        cart.ProductCarts.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6),
                                            Product = new()
                                            {
                                                Id = subReader.GetInt32(7),
                                                Code = subReader.GetString(8),
                                                Name = subReader.GetString(9),
                                                Price = subReader.GetDecimal(10),
                                                Stock = subReader.GetInt32(11)
                                            }
                                        });
                                    }
                                }

                                //  ...updates the cart values...
                                decimal subtotal = EstimateSubtotal(cart);
                                decimal shipping = EstimateShipping(cart);
                                decimal discount = EstimateDiscount(cart);
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot update the cart values.")
                                        );
                                }

                                cart.Subtotal = subtotal;
                                cart.Shipping = shipping;
                                cart.Discount = discount;
                                cart.Total = total;

                                //  ...and, finally, adds the resulted cart to the list.
                                carts.Add(cart);
                            }
                            else
                            {
                                //  ...else, throw an exception.
                                transaction.Rollback();

                                throw new Exception(
                                        "Failed to remove the product to the cart.",
                                        new Exception("The given cart does not exists.")
                                    );
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = carts;

            return Result;
        }

        /// <summary>
        ///     Removes all the product of a cart. The cart will not be deleted from the
        /// database, though.
        /// </summary>
        /// 
        /// <param name="_cartId">The ID of the cart.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        public Response<Cart> RemoveAll(int _cartId)
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        //  First, searches the cart.
                        CartEntity cartEntity = new();
                        cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.SQLJoin();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //  If the cart exists, loads the cart...
                                Cart cart = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Subtotal = reader.GetDecimal(1),
                                    Discount = reader.GetDecimal(2),
                                    Shipping = reader.GetDecimal(3),
                                    Total = reader.GetDecimal(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    User = new()
                                    {
                                        Id = reader.GetInt32(8),
                                        Username = reader.GetString(9),
                                        Name = reader.GetString(10)
                                    },
                                    Coupon = !reader.IsDBNull(11) ? new()
                                    {
                                        Id = reader.GetInt32(11),
                                        Code = reader.GetString(12),
                                        Description = reader.GetString(13),
                                        Discount = reader.GetDecimal(14)
                                    } : null,
                                    ProductCarts = new()
                                };

                                //  ...finds all the products from the cart...
                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        cart.ProductCarts.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6),
                                            Product = new()
                                            {
                                                Id = subReader.GetInt32(7),
                                                Code = subReader.GetString(8),
                                                Name = subReader.GetString(9),
                                                Price = subReader.GetDecimal(10),
                                                Stock = subReader.GetInt32(11)
                                            }
                                        });
                                    }
                                }

                                //  ...removes all the products from the cart...
                                productCartEntity = new();
                                productCartEntity.Filters.CartId = _cartId;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLDelete();

                                if (subCommand.ExecuteNonQuery() == 0)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to remove all products from the cart.",
                                            new Exception("The cart is already empty.")
                                        );
                                }

                                //  ...updates the products stocks...
                                foreach (ProductCart productCart in cart.ProductCarts)
                                {
                                    if (productCart.Product != null)
                                    {
                                        ProductEntity productEntity = new();
                                        productEntity.Filters.Id = productCart.ProductId;
                                        productEntity.Values.Stock = productCart.Quantity + productCart.Product.Stock;

                                        subCommand = db.CreateCommand();
                                        subCommand.Transaction = transaction;
                                        subCommand.CommandText = productEntity.SQLUpdate();

                                        if (subCommand.ExecuteNonQuery() != 1)
                                        {
                                            transaction.Rollback();

                                            throw new Exception(
                                                    "Failed to remove all products from the cart.",
                                                    new Exception("Cannot update the product stock.")
                                                );
                                        }
                                    } 
                                    else
                                    {
                                        transaction.Rollback();

                                        throw new Exception(
                                                "Failed to remove all products from the cart.",
                                                new Exception("There was a problem finding the producs on the cart.")
                                            );
                                    }                                    
                                }

                                cart.ProductCarts.Clear();

                                //  ...updates the cart values...
                                decimal subtotal = EstimateSubtotal(cart);
                                decimal shipping = EstimateShipping(cart);
                                decimal discount = EstimateDiscount(cart);
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                           "Failed to remove all products from the cart.",
                                            new Exception("Cannot update the cart values.")
                                        );
                                }

                                cart.Subtotal = subtotal;
                                cart.Shipping = shipping;
                                cart.Discount = discount;
                                cart.Total = total;

                                //  ...and, finally, adds the resulted cart to the list.
                                carts.Add(cart);
                            }
                            else
                            {
                                //  ...else, throw an exception.
                                transaction.Rollback();

                                throw new Exception(
                                        "Failed to remove all products from the cart.",
                                        new Exception("The given cart does not exists.")
                                    );
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = carts;

            return Result;
        }

        /// <summary>
        ///     Applies a coupon to a cart, adding a discount to it. The coupon must exists,
        /// and any coupon added previously will be discarded.
        /// </summary>
        /// 
        /// <param name="_cartId">The ID of the cart.</param>
        /// <param name="_coupon">The code of the coupon.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        public Response<Cart> ApplyCoupon(int _cartId, string _coupon)
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        //  First, searches the cart.
                        CartEntity cartEntity = new();
                        cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.SQLJoin();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //  If the cart exists, loads the cart...
                                Cart cart = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Subtotal = reader.GetDecimal(1),
                                    Discount = reader.GetDecimal(2),
                                    Shipping = reader.GetDecimal(3),
                                    Total = reader.GetDecimal(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    User = new()
                                    {
                                        Id = reader.GetInt32(8),
                                        Username = reader.GetString(9),
                                        Name = reader.GetString(10)
                                    },
                                    Coupon = !reader.IsDBNull(11) ? new()
                                    {
                                        Id = reader.GetInt32(11),
                                        Code = reader.GetString(12),
                                        Description = reader.GetString(13),
                                        Discount = reader.GetDecimal(14)
                                    } : null,
                                    ProductCarts = new()
                                };

                                //  ...verifies if the applied coupon is the same...
                                if (cart.Coupon != null && FormatCoupon(cart.Coupon.Code) == FormatCoupon(_coupon))
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to apply the coupon to the cart.",
                                            new Exception("This coupon is already applied to the cart.")
                                        );
                                }

                                //  ...validates the given coupon...
                                CouponEntity couponEntity = new();
                                couponEntity.Filters.Code = FormatCoupon(_coupon);

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = couponEntity.SQLSelect();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    if (subReader.Read())
                                    {
                                        cart.Coupon = new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Code = subReader.GetString(1),
                                            Description = subReader.GetString(2),
                                            Discount = subReader.GetDecimal(3)
                                        };
                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        throw new Exception(
                                                "Failed to apply the coupon to the cart.",
                                                new Exception("The given coupon is invalid.")
                                            );
                                    }
                                }

                                //  ...applies the coupon to the cart...
                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.CouponId = cart.Coupon.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to apply the coupon to the cart.",
                                            new Exception("Cannot bind the given coupon to the user cart.")
                                        );
                                }

                                //  ...gets the list of the products on the cart...
                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        cart.ProductCarts.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6),
                                            Product = new()
                                            {
                                                Id = subReader.GetInt32(7),
                                                Code = subReader.GetString(8),
                                                Name = subReader.GetString(9),
                                                Price = subReader.GetDecimal(10),
                                                Stock = subReader.GetInt32(11)
                                            }
                                        });
                                    }
                                }

                                //  ...updates the cart values...
                                decimal subtotal = EstimateSubtotal(cart);
                                decimal shipping = EstimateShipping(cart);
                                decimal discount = EstimateDiscount(cart);
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to apply the coupon to the cart.",
                                            new Exception("Cannot update the cart values.")
                                        );
                                }

                                cart.Subtotal = subtotal;
                                cart.Shipping = shipping;
                                cart.Discount = discount;
                                cart.Total = total;

                                //  ...and, finally, adds the resulted cart to the list.
                                carts.Add(cart);
                            }
                            else
                            {
                                //  ...else, throw an exception.
                                transaction.Rollback();

                                throw new Exception(
                                        "Failed to apply the coupon to the cart.",
                                        new Exception("The given cart does not exists.")
                                    );
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = carts;

            return Result;
        }

        /// <summary>
        ///     Clears the coupon from a cart.
        /// </summary>
        /// 
        /// <param name="_cartId">The ID of the cart.</param>
        /// 
        /// <returns>
        ///     A response with the corresponding cart from the database.
        /// </returns>
        public Response<Cart> ClearCoupon(int _cartId)
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        //  First, searches the cart.
                        CartEntity cartEntity = new();
                        cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.SQLJoin();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //  If the cart exists, loads the cart...
                                Cart cart = new()
                                {
                                    Id = reader.GetInt32(0),
                                    Subtotal = reader.GetDecimal(1),
                                    Discount = reader.GetDecimal(2),
                                    Shipping = reader.GetDecimal(3),
                                    Total = reader.GetDecimal(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    User = new()
                                    {
                                        Id = reader.GetInt32(8),
                                        Username = reader.GetString(9),
                                        Name = reader.GetString(10)
                                    },                                    
                                    ProductCarts = new()
                                };

                                //  ...verifies if really exists a binded coupon to the cart...
                                Coupon? coupon = !reader.IsDBNull(11) ? new()
                                {
                                    Id = reader.GetInt32(11),
                                    Code = reader.GetString(12),
                                    Description = reader.GetString(13),
                                    Discount = reader.GetDecimal(14)
                                } : null;
                                
                                if (coupon == null)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to clear the coupon of the cart.",
                                            new Exception("There is no coupon binded to this cart.")
                                        );
                                }

                                //  ...clear the coupon of the cart...
                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.CouponId = EntityField.NULL_INT;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to clear the coupon of the cart.",
                                            new Exception("Cannot bind the given coupon to the user cart.")
                                        );
                                }

                                //  ...gets the list of the products on the cart...
                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.SQLJoin();

                                using (var subReader = subCommand.ExecuteReader())
                                {
                                    while (subReader.Read())
                                    {
                                        cart.ProductCarts.Add(new()
                                        {
                                            Id = subReader.GetInt32(0),
                                            Price = subReader.GetDecimal(1),
                                            Quantity = subReader.GetInt32(2),
                                            Total = subReader.GetDecimal(3),
                                            AddedAt = subReader.GetDateTime(4),
                                            CartId = subReader.GetInt32(5),
                                            ProductId = subReader.GetInt32(6),
                                            Product = new()
                                            {
                                                Id = subReader.GetInt32(7),
                                                Code = subReader.GetString(8),
                                                Name = subReader.GetString(9),
                                                Price = subReader.GetDecimal(10),
                                                Stock = subReader.GetInt32(11)
                                            }
                                        });
                                    }
                                }

                                //  ...updates the cart values...
                                decimal subtotal = EstimateSubtotal(cart);
                                decimal shipping = EstimateShipping(cart);
                                decimal discount = EstimateDiscount(cart);
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.SQLUpdate();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to apply the coupon to the cart.",
                                            new Exception("Cannot update the cart values.")
                                        );
                                }

                                cart.Subtotal = subtotal;
                                cart.Shipping = shipping;
                                cart.Discount = discount;
                                cart.Total = total;

                                //  ...and, finally, adds the resulted cart to the list.
                                carts.Add(cart);
                            }
                            else
                            {
                                //  ...else, throw an exception.
                                transaction.Rollback();

                                throw new Exception(
                                        "Failed to apply the coupon to the cart.",
                                        new Exception("The given cart does not exists.")
                                    );
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = carts;

            return Result;
        }

        /// <summary>
        ///     Estimates the subtotal for a cart.
        /// </summary>
        /// 
        /// <param name="_cart">The given cart.</param>
        /// 
        /// <returns>A decimal representing the subtotal of the cart.</returns>
        private static decimal EstimateSubtotal(Cart _cart)
        {
            return _cart.ProductCarts.Sum(p => p.Total);
        }

        /// <summary>
        ///     Estimates the shipping cost for a cart. The shipping cost changes
        /// depending of the products in the cart.
        /// </summary>
        /// 
        /// <param name="_cart">The given cart.</param>
        /// 
        /// <returns>A decimal representing the shipping of the cart.</returns>
        private static decimal EstimateShipping(Cart _cart)
        {
            decimal shipping = 0;
            decimal quantity = _cart.ProductCarts.Count;
            decimal total = _cart.ProductCarts.Sum(p => p.Total);

            if (quantity > 0)
            {   
                //  Hey! It's an empty cart!
                shipping = decimal.Zero;
            }
            if (quantity >= 5)
            {
                //  Free shipping for 5 products or more!
                shipping = 0;
            }
            else if (total >= 200)
            {
                //  Free shipping for spending $200 or more!
                shipping = 0;
            } else if (total >= 100)
            {
                //  Shipping of %9 for spending $100 or more!
                shipping = Math.Round(total * .09m, 2);
            } else
            {
                //  Shipping of %12 by default!
                shipping = Math.Round(total * .12m, 2);
            }

            return shipping;
        }

        /// <summary>
        ///     Estimates the discount for a cart (generally given by a coupon).
        /// </summary>
        /// 
        /// <param name="_cart">The given cart.</param>
        /// 
        /// <returns>A decimal representing the discount of the cart.</returns>
        private static decimal EstimateDiscount(Cart _cart)
        {
            decimal discount = 0;

            if (_cart.Coupon != null)
            {
                discount = Math.Round(_cart.Subtotal * _cart.Coupon.Discount, 2);
            }

            return discount;
        }

        /// <summary>
        ///     Formats the coupon code in order to avoid any text conflicts.
        /// </summary>
        /// 
        /// <param name="_coupon">The code of the coupon.</param>
        /// 
        /// <returns>The code of the coupon, formatted.</returns>
        private static string FormatCoupon(string _coupon)
        {
            return _coupon.ToUpper().Trim();
        }
    }
}
