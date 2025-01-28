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

