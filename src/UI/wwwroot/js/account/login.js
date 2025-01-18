function validateUserName() {
    var userNameInput = document.getElementById('txtUser');
    var feedbackElement = userNameInput.nextElementSibling;
    if (!userNameInput.value.trim()) {
        userNameInput.classList.add('is-invalid');
        feedbackElement.innerText = 'Por favor, preencha o campo login';
    } else {
        userNameInput.classList.remove('is-invalid');
        feedbackElement.innerText = '';
        userNameInput.classList.add('is-valid');
    }
    formIsValid();
}
function validatePassword() {
    var passwordInput = document.getElementById('txtPassword');
    var feedbackElement = passwordInput.nextElementSibling;
    if (!passwordInput.value.trim()) {
        passwordInput.classList.add('is-invalid');
        feedbackElement.innerText = 'Por favor, preencha o campo senha';
    } else {
        passwordInput.classList.remove('is-invalid');
        feedbackElement.innerText = '';
        passwordInput.classList.add('is-valid');
    }
    formIsValid();
}
function formIsValid() {
    var userNameInput = document.getElementById('txtUser');
    var passwordInput = document.getElementById('txtPassword');
    var submitBtn = document.getElementById('btnSubmit');
    if (userNameInput.value.trim() && passwordInput.value.trim()) {
        // Se ambos os campos estão preenchidos, ative o botão de envio
        //submitBtn.disabled = false;
    } else {
        // Caso contrário, desative o botão de envio
        //submitBtn.disabled = true;
    }
}