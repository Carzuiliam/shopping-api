using Shopping_API.Entities.Base;
using Shopping_API.Entities.Connection;
using Shopping_API.Entities.Extended;
using Shopping_API.Models;
using Shopping_API.Utils;

namespace Shopping_API.Api.Handler
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
            EntityDB entityDB = new();

            try
            {
                entityDB.Start();

                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);

                Result.Data = cartEntity.Select(entityDB);

                foreach (Cart cart in Result.Data)
                {
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCart = productCartEntity.Select(entityDB);
                }

                entityDB.Finish();
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
            EntityDB entityDB = new();

            try
            {
                entityDB.Start();

                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.Id = _cartId;

                Result.Data = cartEntity.Select(entityDB);

                foreach (Cart cart in Result.Data)
                {
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCart = productCartEntity.Select(entityDB);
                }

                entityDB.Finish();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

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
            EntityDB entityDB = new();

            try
            {
                entityDB.Start();

                //  First, searches the cart.
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.UserId = _userId;

                Cart? cart = cartEntity.Select(entityDB).FirstOrDefault();

                if (cart == null)
                {
                    //  If the cart does not exists, creates a new cart for the user...
                    cartEntity.Values.UserId = _userId;

                    cartEntity.Insert(entityDB);

                    cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                    cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                    cartEntity.Filters.UserId = _userId;

                    Result.Data = cartEntity.Select(entityDB);
                }
                else
                {
                    //  ...otherwise, loads the cart.
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCart = productCartEntity.Select(entityDB);

                    Result.Data = new() { cart };
                }

                entityDB.Finish();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

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
            EntityDB entityDB = new();

            try
            {
                entityDB.Transact();

                //  First, searches the cart.
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.UserId = _cartId;

                Cart? cart = cartEntity.Select(entityDB).FirstOrDefault();

                if (cart != null)
                {
                    //  If the cart exists, verifies if the product has already been added...
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Filters.CartId = _cartId;
                    productCartEntity.Filters.ProductId = _productId;

                    if (productCartEntity.Select(entityDB).Count == 1)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to add the product to the cart.",
                                new Exception(
                                    "The product already exists in the cart. " +
                                    "Consider change its quantity instead."
                                )
                            );
                    }

                    //  ...finds the added product...
                    ProductEntity productEntity = new();
                    productEntity.Filters.Id = _productId;

                    Product? product = productEntity.Select(entityDB).FirstOrDefault();

                    if (product == null)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to add the product to the cart.",
                                new Exception("Cannot not find the given product to add.")
                            );
                    }

                    if (product.Stock <= 0)
                    {
                        entityDB.Rollback();

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

                    if (!productCartEntity.Insert(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to add the product to the cart.",
                                new Exception("Cannot bind a product to the user cart.")
                            );
                    }

                    //  ...updates the product stock...
                    product.Stock--;

                    productEntity.Filters.Id = product.Id;
                    productEntity.Values.Stock = product.Stock;

                    if (!productEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to add the product to the cart.",
                                new Exception("Cannot update the product stock.")
                            );
                    }

                    //  ...gets the (updated) list of the products on the cart...
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCart = productCartEntity.Select(entityDB);
                    
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

                    if (!cartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

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
                    Result.Data = new() { cart };                                
                }
                else
                {
                    //  ...else, throw an exception.
                    entityDB.Rollback();

                    throw new Exception(
                            "Failed to add the product to the cart.",
                            new Exception("The given cart does not exists.")
                        );
                }

                entityDB.Commit();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

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
            EntityDB entityDB = new();

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

                entityDB.Transact();

                //  First, searches the cart.
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.UserId = _cartId;

                Cart? cart = cartEntity.Select(entityDB).FirstOrDefault();

                if (cart != null)
                {
                    //  If the cart exists, loads the products in the cart...
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    List<ProductCart> productsCart = productCartEntity.Select(entityDB);

                    if (productsCart.Count == 0)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to change the quantity of a product in the cart.",
                                new Exception("The cart is already empty.")
                            );
                    }

                    //  ...verifies if the product exists on cart...
                    ProductCart? productCart = productsCart.FirstOrDefault(p => p.ProductId == _productId);

                    if (productCart == null)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to change the quantity of a product in the cart.",
                                new Exception("The given product does not exists inside the given cart.")
                            );
                    }

                    //  ...finds the given product and verifies if it is possible to change its quantity...
                    int fullStock = 0;

                    if (productCart.Product != null)
                    {
                        fullStock = productCart.Quantity + productCart.Product.Stock;

                        if (fullStock < _quantity)
                        {
                            entityDB.Rollback();
                        
                            throw new Exception(
                                "Failed to change the quantity of a product in the cart.",
                                new Exception("There is not enough stock to update the product quantity.")
                            );
                        }                                               
                    }
                    else
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to change the quantity of a product in the cart.",
                                new Exception("Cannot find the given product to change its quantity.")
                            );
                    }

                    //  ...updates the product cart quantity and total...
                    productCartEntity.Filters.Id = productCart.Id;
                    productCartEntity.Values.Quantity = _quantity;
                    productCartEntity.Values.Total = productCart.Price * _quantity;

                    if (!productCartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to change the quantity of a product in the cart.",
                                new Exception("Cannot update the product cart stock.")
                            );
                    }

                    //  ...updates the product stock...
                    ProductEntity productEntity = new();
                    productEntity.Filters.Id = productCart.ProductId;
                    productEntity.Values.Stock = fullStock - _quantity;

                    if (!productEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to change the quantity of a product in the cart.",
                                new Exception("Cannot update the product stock.")
                            );
                    }

                    //  ...gets the (updated) list of the products on the cart...
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCart = productCartEntity.Select(entityDB);

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

                    if (!cartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to change the quantity of a product in the cart.",
                                new Exception("Cannot update the cart values.")
                            );
                    }

                    cart.Subtotal = subtotal;
                    cart.Shipping = shipping;
                    cart.Discount = discount;
                    cart.Total = total;

                    //  ...and, finally, adds the resulted cart to the list.
                    Result.Data = new() { cart };
                }
                else
                {
                    //  ...else, throw an exception.
                    entityDB.Rollback();

                    throw new Exception(
                            "Failed to change the quantity of a product in the cart.",
                            new Exception("The given cart does not exists.")
                        );
                }

                entityDB.Commit();                   
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

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
            EntityDB entityDB = new();

            try
            {
                entityDB.Transact();

                //  First, searches the cart.
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.UserId = _cartId;

                Cart? cart = cartEntity.Select(entityDB).FirstOrDefault();

                if (cart != null)
                {
                    //  If the cart exists, loads the products in the cart...
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Filters.CartId = cart.Id;

                    List<ProductCart> productsCart = productCartEntity.Select(entityDB);

                    if (productsCart.Count == 0)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to remove the product to the cart.",
                                new Exception("The cart is already empty.")
                            );
                    }

                    //  ...verifies if the product exists on cart...
                    ProductCart? productCart = productsCart.FirstOrDefault(p => p.ProductId == _productId);

                    if (productCart == null)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to remove the product to the cart.",
                                new Exception("The given product does not exists inside the given cart.")
                            );
                    }

                    //  ...removes the product from the cart...
                    productCartEntity.Filters.Id = productCart.Id;

                    if (!productCartEntity.Delete(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to remove the product to the cart.",
                                new Exception("Cannot remove the given product from the user cart.")
                            );
                    }

                    //  ...finds the removed product...
                    ProductEntity productEntity = new();
                    productEntity.Filters.Id = productCart.ProductId;

                    Product? product = productEntity.Select(entityDB).FirstOrDefault();
                    
                    if (product == null) 
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to add the product to the cart.",
                                new Exception("Cannot find the given product to add.")
                            );
                    }

                    //  ...updates the product stock...
                    product.Stock++;

                    productEntity.Filters.Id = product.Id;
                    productEntity.Values.Stock = product.Stock;

                    if (!productEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to add the product to the cart.",
                                new Exception("Cannot update the product stock.")
                            );
                    }

                    //  ...gets the (updated) list of the products on the cart...
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCart = productCartEntity.Select(entityDB);

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

                    if (!cartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

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
                    Result.Data = new() { cart };
                }
                else
                {
                    //  ...else, throw an exception.
                    entityDB.Rollback();

                    throw new Exception(
                            "Failed to remove the product to the cart.",
                            new Exception("The given cart does not exists.")
                        );
                }

                entityDB.Commit();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

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
            EntityDB entityDB = new();

            try
            {
                entityDB.Transact();

                //  First, searches the cart.
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.UserId = _cartId;

                Cart? cart = cartEntity.Select(entityDB).FirstOrDefault();

                if (cart != null)
                {
                    //  If the cart exists, loads the products in the cart...
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    List<ProductCart> productsCart = productCartEntity.Select(entityDB);

                    if (productsCart.Count == 0)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to remove all products from the cart.",
                                new Exception("The cart is already empty.")
                            );
                    }

                    //  ...removes all the products from the cart...
                    productCartEntity.Filters.CartId = cart.Id;

                    if (!productCartEntity.Delete(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to remove all products from the cart.",
                                new Exception("There was a problem removing all the products from the cart.")
                            );
                    }

                    //  ...updates the stock for each product...
                    foreach (ProductCart prodCart in productsCart)
                    {
                        if (prodCart.Product == null)
                        {
                            entityDB.Rollback();

                            throw new Exception(
                                    "Failed to remove all products from the cart.",
                                    new Exception("Cannot find the product associated with a product cart.")
                                );
                        }

                        ProductEntity productEntity = new();
                        productEntity.Filters.Id = prodCart.ProductId;
                        productEntity.Values.Stock = prodCart.Quantity + prodCart.Product.Stock;

                        if (!productEntity.Update(entityDB))
                        {
                            entityDB.Rollback();

                            throw new Exception(
                                    "Failed to remove all products from the cart.",
                                    new Exception("There was a problem updating the stocks of the products.")
                                );
                        }
                    }

                    cart.ProductCart.Clear();

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

                    if (!cartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

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
                    Result.Data = new() { cart };
                }
                else
                {
                    //  ...else, throw an exception.
                    entityDB.Rollback();

                    throw new Exception(
                            "Failed to remove all products from the cart.",
                            new Exception("The given cart does not exists.")
                        );
                }

                entityDB.Commit();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

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
            EntityDB entityDB = new();

            try
            {
                entityDB.Transact();

                //  First, searches the cart.
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.UserId = _cartId;

                Cart? cart = cartEntity.Select(entityDB).FirstOrDefault();

                if (cart != null)
                {
                    //  If the cart exists, verifies if the applied coupon is the same...
                    if (cart.Coupon != null && FormatCoupon(cart.Coupon.Code) == FormatCoupon(_coupon))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to apply the coupon to the cart.",
                                new Exception("This coupon is already applied to the cart.")
                            );
                    }

                    //  ...verifies if the coupon is valid...
                    CouponEntity couponEntity = new();
                    couponEntity.Filters.Code = FormatCoupon(_coupon);

                    Coupon? coupon = couponEntity.Select(entityDB).FirstOrDefault();

                    if (coupon == null)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to apply the coupon to the cart.",
                                new Exception("The given coupon is invalid/does not exists.")
                            );
                    }

                    //  ...applies the coupon to the cart...
                    cartEntity.Filters.Id = cart.Id;
                    cartEntity.Values.CouponId = coupon.Id;

                    if (!cartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to apply the coupon to the cart.",
                                new Exception("Cannot update the coupon in the cart.")
                            );
                    }

                    cart.Coupon = coupon;

                    //  ...gets the list of the products on the cart...
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCart = productCartEntity.Select(entityDB);

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

                    if (!cartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

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
                    Result.Data = new() { cart };
                }
                else
                {
                    //  ...else, throw an exception.
                    entityDB.Rollback();

                    throw new Exception(
                            "Failed to apply the coupon to the cart.",
                            new Exception("The given cart does not exists.")
                        );
                }

                entityDB.Commit();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

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
            EntityDB entityDB = new();

            try
            {
                entityDB.Transact();

                //  First, searches the cart.
                CartEntity cartEntity = new();
                cartEntity.Relations.Bind(new UserEntity(), EntityRelation.RelationMode.FULL);
                cartEntity.Relations.Bind(new CouponEntity(), EntityRelation.RelationMode.OPTIONAL);
                cartEntity.Filters.UserId = _cartId;

                Cart? cart = cartEntity.Select(entityDB).FirstOrDefault();

                if (cart != null)
                {
                    //  If the cart exists, verifies if there is a coupon applied in it...
                    if (cart.Coupon == null)
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to remove the coupon from the cart.",
                                new Exception("There is no coupon in the cart.")
                            );
                    }

                    //  ...applies the coupon to the cart...
                    cartEntity.Filters.Id = cart.Id;
                    cartEntity.Values.CouponId = EntityField.NULL_INT;

                    if (!cartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

                        throw new Exception(
                                "Failed to remove the coupon from the cart.",
                                new Exception("Cannot update the coupon in the cart.")
                            );
                    }

                    cart.Coupon = null;

                    //  ...gets the list of the products on the cart...
                    ProductCartEntity productCartEntity = new();
                    productCartEntity.Relations.Bind(new ProductEntity(), EntityRelation.RelationMode.FULL);
                    productCartEntity.Filters.CartId = cart.Id;

                    cart.ProductCart = productCartEntity.Select(entityDB);

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

                    if (!cartEntity.Update(entityDB))
                    {
                        entityDB.Rollback();

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
                    Result.Data = new() { cart };
                }
                else
                {
                    //  ...else, throw an exception.
                    entityDB.Rollback();

                    throw new Exception(
                            "Failed to apply the coupon to the cart.",
                            new Exception("The given cart does not exists.")
                        );
                }

                entityDB.Commit();
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

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
            return _cart.ProductCart.Sum(p => p.Total);
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
            decimal quantity = _cart.ProductCart.Count;
            decimal total = _cart.ProductCart.Sum(p => p.Total);

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
            return (_cart.Coupon != null) ?
                Math.Round(_cart.ProductCart.Sum(p => p.Price * p.Quantity) * _cart.Coupon.Discount, 2) :
                0;
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
