function formatZipCode(zipCode) {
    zipCode = zipCode.replace(/\D/g, ''); // Remove todos os caracteres não numéricos
    zipCode = zipCode.replace(/^(\d{5})(\d)/, '$1-$2'); // Adiciona um hífen após os primeiros 5 dígitos
    return zipCode;
}

function formatPhone(phone) {
    phone = phone.replace(/\D/g, ''); // Remove todos os caracteres não numéricos
    phone = phone.replace(/^(\d{2})(\d)/g, '($1) $2'); // Adiciona parênteses em torno do DDD
    phone = phone.replace(/(\d)(\d{4})$/, '$1-$2'); // Adiciona um hífen antes dos últimos 4 dígitos
    return phone;
}


function formatCNPJ(cnpj) {
    cnpj = cnpj.replace(/\D/g, ''); // Remove todos os caracteres não numéricos
    cnpj = cnpj.replace(/^(\d{2})(\d)/, '$1.$2'); // Adiciona um ponto após os primeiros 2 dígitos
    cnpj = cnpj.replace(/^(\d{2})\.(\d{3})(\d)/, '$1.$2.$3'); // Adiciona um ponto após os próximos 3 dígitos
    cnpj = cnpj.replace(/\.(\d{3})(\d)/, '.$1/$2'); // Adiciona uma barra após os próximos 3 dígitos
    cnpj = cnpj.replace(/(\d{4})(\d)/, '$1-$2'); // Adiciona um hífen antes dos últimos 2 dígitos
    return cnpj;
}

function formatCNPJRoot(cnpjRoot) {
    cnpjRoot = cnpjRoot.replace(/\D/g, ''); // Remove todos os caracteres não numéricos
    cnpjRoot = cnpjRoot.replace(/^(\d{2})(\d)/, '$1.$2'); // Adiciona um ponto após os primeiros 2 dígitos
    cnpjRoot = cnpjRoot.replace(/^(\d{2})\.(\d{3})(\d)/, '$1.$2.$3'); // Adiciona um ponto após os próximos 3 dígitos
    cnpjRoot = cnpjRoot.replace(/\.(\d{3})(\d)/, '.$1'); // Adiciona um ponto após os próximos 3 dígitos
    return cnpjRoot;
}

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


var companyModal = new bootstrap.Modal(document.getElementById('companyCreateModal'), {
    keyboard: false
});

var companyFailureModal = new bootstrap.Modal(document.getElementById('companyCreateFailureModal'), {
    keyboard: false
});

function showModal() {
    companyModal.show();
}

function showFailureModal() {
    companyFailureModal.show();
}