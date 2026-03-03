// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Image upload preview
function previewImage(event) {
    const preview = document.getElementById('imagePreview');
    if (preview && event.target.files[0]) {
        preview.src = URL.createObjectURL(event.target.files[0]);
        preview.style.display = 'block';
    }
}
//```

//---

//### `Data / cars.csv` *(initial file — seeded with 2 sample cars)*
//```
//Id, Make, Model, Year, Price, Mileage, FuelType, Transmission, Color, Description, ImagePath, IsAvailable, DateAdded
//1, Toyota, Camry, 2022, 18500000, 32000, Petrol, Automatic, Pearl White, "A pristine 2022 Toyota Camry in excellent condition. Low mileage, full service history, and all original parts.", /images/cars /default.jpg, True, 2024-01 - 15
//2, Honda, Accord, 2021, 16200000, 45000, Petrol, Automatic, Midnight Black, "A well-maintained 2021 Honda Accord Sport Edition. Loaded with features including sunroof, leather seats, and advanced safety systems.", /images/cars /default.jpg, True, 2024-01 - 20
//    ```

//### `Data / inquiries.csv` *(initial empty file)*
//```
//Id, CarId, Name, Email, Phone, Message, DateSubmitted