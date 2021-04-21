let form = document.forms[0];

form.addEventListener("submit", function (event) {
    event.preventDefault();
    new FormData(form);
});

form.addEventListener("formdata", event => {
    const data = event.formData;
    const values = [...data.values()];

    console.log(values);

    let email = data.get("email");
    let password = data.get("password");
    let repeatedPassword = data.get("repeatPassword");

    if (email.length == 0 || password == 0 || repeatedPassword == 0) {
        Swal.fire('Одно из полей не заполнено!', 'Проверьте введенные данные', 'error');
        return;
    }
    if (password != repeatedPassword) {
        Swal.fire('Пароли не совпадают!', '', 'error');
        return;
    }
    if (password < 8) {
        Swal.fire('Пароль слишком короткий!', '', 'warning');
        return;
    }
    Swal.fire('Поздравляем!', 'Вы успешно прошли регистрацию!', 'success');
});