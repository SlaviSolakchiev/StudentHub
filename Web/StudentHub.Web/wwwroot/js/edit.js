function IsChecked() {
    let btn = document.querySelector("button");    
    const checkboxes = document.querySelectorAll('.form-check input[type="checkbox"]');
    // Филтрирай само отметнатите чекбоксове
    const selectedValues = Array.from(checkboxes)
        .filter(checkbox => checkbox.checked)
        .map(checkbox => checkbox.value);
    console.log(selectedValues); // Или направи нещо друго с избраните стойности
    document.getElementById('selectedRoles').value = Array.from(selectedValues);
    ;
}
