using Microsoft.EntityFrameworkCore;
using SixteenClothing.App.Constants;
using SixteenClothing.App.Contexts;
using SixteenClothing.App.Extensions;
using SixteenClothing.App.Models;
using SixteenClothing.App.Services.Interfaces;
using SixteenClothing.App.ViewModels.Pagination;
using SixteenClothing.App.ViewModels.Slider;

namespace SixteenClothing.App.Services.Implements
{
    public class SliderService : ISliderService
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        readonly IHttpContextAccessor _accessor;

        public SliderService(AppDbContext context, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            _context = context;
            _env = env;
            _accessor = accessor;
        }

        public async Task CreateAsync(SliderCreateVM vm)
        {
            var slider = new Slider()
            {
                Heading = vm.Heading,
                Text = vm.Text,
                CreatedAt = TimeZones.AzerbaijaniTime
            };
            var result = vm.Image.IsCorrectFormat("jpg", "png", "jpeg");
            if (!result) throw new Exception("incorrect format");
            result = vm.Image.IsSizeOk(2);
            if (!result) throw new Exception("size exceeds");
            slider.ImageName = await vm.Image.UploadFile(_env.WebRootPath, FilePaths.SliderPath);
            slider.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/{FilePaths.SliderPath}/{slider.ImageName}";
            await _context.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginationViewModel<SliderGetVM>> GetAllAsync(int page, int size)
        {
            var totalCount = await _context.Sliders.CountAsync();
            var query = await _context.Sliders.OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(slider => slider.ToSliderGetVM()).ToListAsync();
            var paginatedVM = new PaginationViewModel<SliderGetVM>(query, totalCount, page, size);

            return paginatedVM;
        }

        public async Task<List<SliderGetVM>> GetAllAsync()
        {
            return await _context.Sliders.AsQueryable().Select(slider => slider.ToSliderGetVM()).ToListAsync();
        }

        public async Task<SliderGetVM> GetSingleAsync(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) throw new Exception("Slider is not found");
            return slider.ToSliderGetVM();
        }

        public async Task RemoveAsync(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) throw new Exception("Slider is not found");
            _context.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SliderUpdateVM vm)
        {
            var slider = await _context.Sliders.FindAsync(vm.Id);
            if (slider == null) throw new Exception("Slider is not found");
            slider.Heading = vm.Heading != null ? vm.Heading : slider.Heading;
            slider.Text = vm.Text != null ? vm.Text : slider.Text;
            slider.UpdatedAt = TimeZones.AzerbaijaniTime;
            _context.Update(slider);
            await _context.SaveChangesAsync();
        }
    }
}
