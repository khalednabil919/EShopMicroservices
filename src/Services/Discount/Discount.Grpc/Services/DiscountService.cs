using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            try
            {

                var list = dbContext.Coupons.ToList();
                var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

                if (coupon is null)
                    coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Description", Id = 0 };

                logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

                var couponModel = coupon.Adapt<CouponModel>();
                return couponModel;
            }
            catch(Exception ex)
            {
                return new CouponModel { ProductName = ex.Message };
            }
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));

            await dbContext.Coupons.AddAsync(coupon);
            var count = await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName: {productName}, Amount:{amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));


            logger.LogInformation("Discount is successfully updated. ProductName: {productName}, Amount:{amount}", coupon.ProductName, coupon.Amount);

            dbContext.Coupons.Update(coupon);
            var count = await dbContext.SaveChangesAsync();
            return coupon.Adapt<CouponModel>();
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Not Found"));

            logger.LogInformation("Discount is successfully deleted. ProductName: {productName}, Amount:{amount}", coupon.ProductName, coupon.Amount);

            dbContext.Remove(coupon);
            await dbContext.SaveChangesAsync();

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
