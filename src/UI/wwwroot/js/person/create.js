function formatData(data) {
    data = data.replace(/\D/g, ''); // Remove todos os caracteres não numéricos
    data = data.replace(/(\d{2})(\d)/, '$1/$2'); // Adiciona uma barra após os primeiros 2 dígitos
    data = data.replace(/(\d{2})(\d)/, '$1/$2'); // Adiciona uma barra após os próximos 2 dígitos
    data = data.replace(/(\d{4})(\d)/, '$1/$2'); // Adiciona uma barra após os próximos 4 dígitos
    return data;
}

function formatCPF(cpf) {
    cpf = cpf.replace(/\D/g, ''); // Remove todos os caracteres não numéricos
    cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2'); // Adiciona um ponto após os primeiros 3 dígitos
    cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2'); // Adiciona um ponto após os próximos 3 dígitos
    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2'); // Adiciona um hífen antes dos últimos 2 dígitos
    return cpf;
}

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

var inputCpf = document.getElementById("cpfInput");
inputCpf.addEventListener("input", function () {
    var newValue = formatCPF(inputCpf.value);
    this.value = newValue;
});

var inputPhone = document.getElementById("phoneNumber");
inputPhone.addEventListener("input", function () {
    var newValue = formatPhone(inputPhone.value);
    this.value = newValue;
    console.log(this.value);
});

var phoneResponsible = document.getElementById("inputPhoneResposible");
phoneResponsible.addEventListener("input", function () {
    var newValue = formatPhone(phoneResponsible.value);
    this.value = newValue;
});

var inputZipCode = document.getElementById("zipCodeInput");
inputZipCode.addEventListener("input", function () {
    var newValue = formatZipCode(inputZipCode.value);
    this.value = newValue;
});

var inputBirthDate = document.getElementById("inputBirthDate");
inputBirthDate.addEventListener("input", function () {
    var newValue = formatData(inputBirthDate.value);
    this.value = newValue;
});

var personModal = new bootstrap.Modal(document.getElementById('personCreateModal'), {
    keyboard: false
});

var personFailureModal = new bootstrap.Modal(document.getElementById('personCreateFailureModal'), {
    keyboard: false
});

function showModal() {
    personModal.show();
}

function showFailureModal() {
    personFailureModal.show();
}

var inputSelectCompany = document.getElementById("inputSelectCompanies");

inputSelectCompany.addEventListener("change", function () {
    var companyId = inputSelectCompany.value;
    //onSelectCompanyChanged(dotNetObject, companyId);
})


function onSelectCompanyChanged(dotNetObject, companyId) {
    dotNetObject.invokeMethodAsync('OnSelectCompanyChanged', companyId);
}
