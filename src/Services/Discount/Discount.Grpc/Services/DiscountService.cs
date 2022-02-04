using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository ?? throw new ArgumentException(nameof(discountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"Discount with ProductName={request.ProductName}"));
            }
            _logger.LogInformation("Discount is retrieved for ProductName: {productName}," +
                "Amount: {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public async Task<CouponModel> GreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _discountRepository.CreateDiscount(coupon);

            _logger.LogInformation
                ("Discount is successfully created. {ProductName}:",
                coupon.ProductName);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _discountRepository.UpdateDiscount(coupon);

            _logger.LogInformation
                ("Discount is successfully updated. {ProductName}:",
                coupon.ProductName);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };
            return response;
        }
    }
}
