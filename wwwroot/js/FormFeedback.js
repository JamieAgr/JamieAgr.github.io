document.addEventListener('DOMContentLoaded', function () {
    const emailInput = document.getElementById('Email');
    const emailError = document.getElementById('email-error');

    emailInput.addEventListener('input', function () {
        const email = emailInput.value.trim();
        const emailRegex = /(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;

        if (!emailRegex.test(email) || email.length > 80) {
            emailInput.style.borderColor = 'red';
            emailError.textContent = 'Please enter a valid email address.';
        } else {
            emailInput.style.borderColor = ''; // Reset border color
            emailError.textContent = ''; // Clear error message
        }
    });

    const firstNameInput = document.getElementById('FirstName');
    const firstNameError = document.getElementById('firstname-error');


    const lastNameInput = document.getElementById('LastName');
    const lastNameError = document.getElementById('lastname-error');

    const PhonenumberInput = document.getElementById('Phonenumber');
    const PhonenumberError = document.getElementById('phonenumber-error');

    firstNameInput.addEventListener('input', function () {
        const firstname = firstNameInput.value.trim();

        if (firstname.length === 0 || firstname.length > 60) {
            firstNameInput.style.borderColor = 'red';
            firstNameError.textContent = 'Please enter a valid firstname (up to 60 characters).';
        } else {
            firstNameInput.style.borderColor = ''; // Reset border color
            firstNameError.textContent = ''; // Clear error message
        }
    });

    lastNameInput.addEventListener('input', function () {
        const lastname = lastNameInput.value.trim();

        if (lastname.length === 0 || lastname.length > 60) {
            lastNameInput.style.borderColor = 'red';
            lastNameError.textContent = 'Please enter a valid lastname (up to 60 characters).';
        } else {
            lastNameInput.style.borderColor = ''; // Reset border color
            lastNameError.textContent = ''; // Clear error message
        }
    });

    PhonenumberInput.addEventListener('input', function () {
        const phonenumber = PhonenumberInput.value.trim();
        const phoneRegex = /^[0-9]{10}$/;

        if (!phoneRegex.test(phonenumber)) {
            PhonenumberInput.style.borderColor = 'red';
            PhonenumberError.textContent = 'Please enter a valid phonenumber (0612345678).';
        } else {
            PhonenumberInput.style.borderColor = ''; // Reset border color
            PhonenumberError.textContent = ''; // Clear error message
        }
    });

});
