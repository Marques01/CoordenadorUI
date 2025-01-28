var inputCNPJ = document.getElementById("coorporateTaxPayer");
inputCNPJ.addEventListener("input", function () {
    var newValue = formatCNPJ(inputCNPJ.value);
    this.value = newValue;
});

var inputCNPJRoot = document.getElementById("coorporateTaxPayerRoot");
inputCNPJRoot.addEventListener("input", function () {
    var newValue = formatCNPJRoot(inputCNPJRoot.value);
    this.value = newValue;
});

var inputPhone = document.getElementById("phone");
inputPhone.addEventListener("input", function () {
    var newValue = formatPhone(inputPhone.value);
    this.value = newValue;
    console.log(this.value);
});

var inputConfirmPhone = document.getElementById("confirmPhone");
inputConfirmPhone.addEventListener("input", function () {
    var newValue = formatPhone(inputConfirmPhone.value);
    this.value = newValue;
});

var inputZipCode = document.getElementById("zipCodeInput");
inputZipCode.addEventListener("input", function () {
    var newValue = formatZipCode(inputZipCode.value);
    this.value = newValue;
});
function showModal() {
    var companyModal = new bootstrap.Modal(document.getElementById('companyCreateModal'), {
        keyboard: false
    });
    companyModal.show();
}

