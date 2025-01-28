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


function showModal() {
    var personModal = new bootstrap.Modal(document.getElementById('personCreateModal'), {
        keyboard: false
    });

    personModal.show();
}

function showFailureCompanyModal(args) {
    var companyFailureModal = new bootstrap.Modal(document.getElementById('companyCreateFailureModal'), {
        keyboard: false
    });

    var txtFailureModal = document.getElementById('txtCompanyCreateFailureModal');
    txtFailureModal.innerHTML = args;

    companyFailureModal.show();
}

function showCompanyModal() {
    var companyModal = new bootstrap.Modal(document.getElementById('companyCreateModal'), {
        keyboard: false
    });
    companyModal.show();
}

function showFailureModal() {
    var personFailureModal = new bootstrap.Modal(document.getElementById('personCreateFailureModal'), {
        keyboard: false
    });

    personFailureModal.show();
}

async function fetchPublicIP() {
    try {
        const response = await fetch('https://api.ipify.org?format=json');
        const data = await response.json();
        console.log('IP Público:', data.ip);
        return data.ip;
    } catch (error) {
        console.error('Erro ao obter o IP:', error);
    }
}

async function getUserAgentData() {
    if ('userAgentData' in navigator) {
        try {
            const data = await navigator.userAgentData.getHighEntropyValues(['model', 'platform']);
            console.log("Modelo do dispositivo:", data.model); // Exemplo: 'Pixel 5', 'iPhone'
            console.log("Plataforma:", data.platform);         // Exemplo: 'Android', 'iOS'
        } catch (err) {
            console.error("Erro ao obter informações:", err);
        }
    } else {
        console.log("User-Agent Client Hints não suportado neste navegador.");
    }
}

function checkGeolocationPermission() {
    if ("geolocation" in navigator && "permissions" in navigator) {
        navigator.permissions.query({ name: "geolocation" }).then((permissionStatus) => {
            console.log(`Permissão atual: ${permissionStatus.state}`);

            if (permissionStatus.state === "granted") {
                getGeolocation();
            } else if (permissionStatus.state === "prompt") {
                requestGeolocation();
            } else if (permissionStatus.state === "denied") {
                console.warn("Permissão para localização foi negada. Por favor, habilite nas configurações do navegador.");
            }

            permissionStatus.onchange = () => {
                console.log(`Nova permissão: ${permissionStatus.state}`);
            };
        });
    } else {
        console.error("Geolocalização ou Permissions API não são suportadas neste navegador.");
    }
}

function getGeolocation() {
    navigator.geolocation.getCurrentPosition(
        (position) => {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;
            const accuracy = position.coords.accuracy; // Precisão em metros
            console.log(`Latitude: ${latitude}, Longitude: ${longitude}, Precisão: ${accuracy} metros`);
        },
        (error) => {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    console.error("Permissão negada pelo usuário.");
                    break;
                case error.POSITION_UNAVAILABLE:
                    console.error("Localização indisponível.");
                    break;
                case error.TIMEOUT:
                    console.error("Tempo para obter localização esgotado.");
                    break;
                default:
                    console.error("Erro desconhecido:", error.message);
            }
        },
        {
            enableHighAccuracy: true, // Solicita o uso de GPS para maior precisão
            timeout: 20000,           // Tempo máximo para obter a localização (20 segundos)
            maximumAge: 0             // Não usa localização em cache
        }
    );
}

function requestGeolocation() {
    navigator.geolocation.getCurrentPosition(
        (position) => {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;
            const accuracy = position.coords.accuracy; // Precisão em metros
            console.log(`Permissão concedida! Latitude: ${latitude}, Longitude: ${longitude}, Precisão: ${accuracy} metros`);
        },
        (error) => {
            if (error.code === error.PERMISSION_DENIED) {
                console.error("Usuário negou a permissão para localização.");
            } else {
                console.error("Erro ao solicitar localização:", error.message);
            }
        },
        {
            enableHighAccuracy: true, // Solicita maior precisão
            timeout: 20000,           // Aumenta o tempo para tentar obter dados mais precisos
            maximumAge: 0             // Não usa dados em cache
        }
    );
}