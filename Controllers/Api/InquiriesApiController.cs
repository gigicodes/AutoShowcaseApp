using AutoShowcaseApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoShowcaseApp.Models;

namespace AutoShowcaseApp.Controllers.Api
{
    [Route("api/inquiries")]
    [ApiController]
    public class InquiriesApiController : ControllerBase
    {
        private readonly IInquiryRepository _inquiries;
        public InquiriesApiController(IInquiryRepository inquiries)
        {
            _inquiries = inquiries;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var inquiries = _inquiries.GetAll();
            return Ok(inquiries);
        }

        [HttpPost]
        public IActionResult Submit([FromBody] Inquiry inquiry)
        {
            _inquiries.Add(inquiry);
            return Ok(new { message = "Inquiry submitted successfully.", id = inquiry.Id });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var inquiry = _inquiries.GetById(id);
            if (inquiry == null)
                return NotFound();
            return Ok(inquiry);
        }
    }
}
