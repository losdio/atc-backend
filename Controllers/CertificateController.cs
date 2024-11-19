using atc_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CertificatesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CertificatesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Generate a certificate
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateCertificate([FromBody] Certificate certificate)
    {
        // Save certificate record in database
        _context.Certificates.Add(certificate);
        await _context.SaveChangesAsync();

        // Generate PDF Certificate
        var pdfPath = Path.Combine("wwwroot/certificates", $"{certificate.Id}_Certificate.pdf");
        GeneratePdf(certificate, pdfPath);
        certificate.CertificatePath = pdfPath;

        // Update the certificate record
        _context.Entry(certificate).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Certificate generated successfully", path = pdfPath });
    }

    // Retrieve all certificates
    [HttpGet]
    public async Task<IActionResult> GetCertificates()
    {
        var certificates = await _context.Certificates.ToListAsync();
        return Ok(certificates);
    }

    // Retrieve a specific certificate by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCertificate(int id)
    {
        var certificate = await _context.Certificates.FindAsync(id);
        if (certificate == null)
            return NotFound();
        return Ok(certificate);
    }

    // Download certificate
    [HttpGet("{id}/download")]
    public IActionResult DownloadCertificate(int id)
    {
        var certificate = _context.Certificates.FirstOrDefault(c => c.Id == id);
        if (certificate == null || string.IsNullOrEmpty(certificate.CertificatePath))
            return NotFound();

        var filePath = certificate.CertificatePath;
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        var fileName = $"{certificate.TraineeName}_Certificate.pdf";
        return File(fileBytes, "application/pdf", fileName);
    }

    // Helper method to generate PDF
    private void GeneratePdf(Certificate certificate, string path)
    {
        using var writer = new iText.Kernel.Pdf.PdfWriter(path);
        using var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
        var document = new iText.Layout.Document(pdf);

        // Add certificate content
        document.Add(new iText.Layout.Element.Paragraph("Certificate of Completion")
            .SetFontSize(24).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
        document.Add(new iText.Layout.Element.Paragraph($"This is to certify that {certificate.TraineeName}")
            .SetFontSize(18).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
        document.Add(new iText.Layout.Element.Paragraph($"has successfully completed the course {certificate.CourseName}")
            .SetFontSize(18).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
        document.Add(new iText.Layout.Element.Paragraph($"Completion Date: {certificate.CompletionDate.ToString("yyyy-MM-dd")}")
            .SetFontSize(18).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

        document.Close();
    }
}
