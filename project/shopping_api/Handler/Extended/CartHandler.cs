using Microsoft.Data.Sqlite;
using shopping_api.Entities.Extended;
using shopping_api.Models;
using shopping_api.Utils;
using Shopping_API.Entities.Filters;

namespace shopping_api.Handler.Default
{
    public class CartHandler : BaseHandler
    {
        public Result<Cart> Result { get; set; }

        public CartHandler()
        {
            Result = new();
        }

        public Result<Cart> List()
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    CartEntity cartEntity = new();
                    cartEntity.Relations.Bind(new UserEntity(), EntityFilter.RelationType.FULL);
                    cartEntity.Relations.Bind(new CouponEntity(), EntityFilter.RelationType.OPTIONAL);

                    SqliteCommand command = db.CreateCommand();
                    command.CommandText = cartEntity.Join();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
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

                            ProductCartEntity productCartEntity = new();
                            productCartEntity.Relations.Bind(new ProductEntity(), EntityFilter.RelationType.FULL);
                            productCartEntity.Filters.CartId = cart.Id;

                            SqliteCommand subCommand = db.CreateCommand();
                            subCommand.CommandText = productCartEntity.Join();

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

        public Result<Cart> Get(int _cartId)
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    CartEntity cartEntity = new();
                    cartEntity.Relations.Bind(new UserEntity(), EntityFilter.RelationType.FULL);
                    cartEntity.Relations.Bind(new CouponEntity(), EntityFilter.RelationType.OPTIONAL);
                    cartEntity.Filters.Id = _cartId;

                    SqliteCommand command = db.CreateCommand();
                    command.CommandText = cartEntity.Join();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
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

                            ProductCartEntity productCartEntity = new();
                            productCartEntity.Relations.Bind(new ProductEntity(), EntityFilter.RelationType.FULL);
                            productCartEntity.Filters.CartId = cart.Id;

                            SqliteCommand subCommand = db.CreateCommand();
                            subCommand.CommandText = productCartEntity.Join();

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

        public Result<Cart> GetFromUser(int _userId)
        {
            List<Cart> carts = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        CartEntity cartEntity = new();
                        cartEntity.Relations.Bind(new UserEntity(), EntityFilter.RelationType.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityFilter.RelationType.OPTIONAL);
                        cartEntity.Filters.UserId = _userId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.Join();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                //  If the cart exists, loads it...
                                if (reader.Read())
                                {
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

                                    ProductCartEntity productCartEntity = new();
                                    productCartEntity.Relations.Bind(new ProductEntity(), EntityFilter.RelationType.FULL);
                                    productCartEntity.Filters.CartId = cart.Id;

                                    SqliteCommand subCommand = db.CreateCommand();
                                    subCommand.Transaction = transaction;
                                    subCommand.CommandText = productCartEntity.Join();

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
                            }
                            else
                            {
                                //  ...else, creates a new cart for the user.
                                cartEntity.Values.UserId = _userId;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.Insert();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to get the cart from the given user.",
                                            new Exception("Cannot create a new cart for the user.")
                                        );
                                }

                                cartEntity.Relations.Bind(new UserEntity(), EntityFilter.RelationType.FULL);
                                cartEntity.Filters.UserId = _userId;

                                subCommand = db.CreateCommand();
                                subCommand.CommandText = cartEntity.Join();

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

        public Result<Cart> AddProduct(int _cartId, int _productId)
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
                        cartEntity.Relations.Bind(new UserEntity(), EntityFilter.RelationType.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityFilter.RelationType.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.Join();

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

                                //  ...verifies if the product has already been added...
                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Filters.CartId = _cartId;
                                productCartEntity.Filters.ProductId = _productId;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.Select();

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
                                subCommand.CommandText = productEntity.Select();

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
                                subCommand.CommandText = productCartEntity.Insert();

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
                                subCommand.CommandText = productEntity.Update();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot update the product stock.")
                                        );
                                }

                                //  ...gets the (updated) list of the products on the cart...
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityFilter.RelationType.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.Join();

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
                                decimal subtotal = cart.ProductCarts.Sum(p => p.Total);
                                decimal shipping = EstimateShipping(cart.ProductCarts);
                                decimal discount = cart.Discount;
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.Update();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot bind the given product to the user cart.")
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

        public Result<Cart> RemoveProduct(int _cartId, int _productId)
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
                        cartEntity.Relations.Bind(new UserEntity(), EntityFilter.RelationType.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityFilter.RelationType.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.Join();

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

                                //  ...loads the products in the cart...
                                List<ProductCart> productsCart = new();

                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Filters.CartId = cart.Id;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.Select();

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
                                subCommand.CommandText = productCartEntity.Delete();

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
                                subCommand.CommandText = productEntity.Select();

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
                                subCommand.CommandText = productEntity.Update();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot update the product stock.")
                                        );
                                }

                                //  ...gets the (updated) list of the products on the cart...
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityFilter.RelationType.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.Join();

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
                                decimal subtotal = cart.ProductCarts.Sum(p => p.Total);
                                decimal shipping = EstimateShipping(cart.ProductCarts);
                                decimal discount = cart.Discount;
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.Update();

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

        public Result<Cart> RemoveAll(int _cartId)
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
                        cartEntity.Relations.Bind(new UserEntity(), EntityFilter.RelationType.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityFilter.RelationType.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.Join();

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

                                //  ...finds all the products from the cart...
                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityFilter.RelationType.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.Join();

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
                                subCommand.CommandText = productCartEntity.Delete();

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
                                        subCommand.CommandText = productEntity.Update();

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
                                decimal subtotal = cart.ProductCarts.Sum(p => p.Total);
                                decimal shipping = EstimateShipping(cart.ProductCarts);
                                decimal discount = cart.Discount;
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.Update();

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

        public Result<Cart> ChangeQuantity(int _cartId, int _productId, int _quantity)
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
                        cartEntity.Relations.Bind(new UserEntity(), EntityFilter.RelationType.FULL);
                        cartEntity.Relations.Bind(new CouponEntity(), EntityFilter.RelationType.OPTIONAL);
                        cartEntity.Filters.Id = _cartId;

                        SqliteCommand command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = cartEntity.Join();

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

                                //  ...finds all the products from the cart...
                                ProductCartEntity productCartEntity = new();
                                productCartEntity.Relations.Bind(new ProductEntity(), EntityFilter.RelationType.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                SqliteCommand subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.Join();

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
                                subCommand.CommandText = productEntity.Update();

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
                                subCommand.CommandText = productCartEntity.Update();

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

                                productCartEntity.Relations.Bind(new ProductEntity(), EntityFilter.RelationType.FULL);
                                productCartEntity.Filters.CartId = cart.Id;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = productCartEntity.Join();

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
                                decimal subtotal = cart.ProductCarts.Sum(p => p.Total);
                                decimal shipping = EstimateShipping(cart.ProductCarts);
                                decimal discount = cart.Discount;
                                decimal total = subtotal + shipping - discount;

                                cartEntity.Filters.Id = cart.Id;
                                cartEntity.Values.Subtotal = subtotal;
                                cartEntity.Values.Shipping = shipping;
                                cartEntity.Values.Discount = discount;
                                cartEntity.Values.Total = total;

                                subCommand = db.CreateCommand();
                                subCommand.Transaction = transaction;
                                subCommand.CommandText = cartEntity.Update();

                                if (subCommand.ExecuteNonQuery() != 1)
                                {
                                    transaction.Rollback();

                                    throw new Exception(
                                            "Failed to add the product to the cart.",
                                            new Exception("Cannot bind the given product to the user cart.")
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

        private static decimal EstimateShipping(List<ProductCart> _products)
        {
            decimal shipping = 0;
            decimal quantity = _products.Count;
            decimal total = _products.Sum(p => p.Total);

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
    }
}
