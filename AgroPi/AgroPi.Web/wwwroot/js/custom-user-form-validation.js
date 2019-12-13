/*User form validation*/
$(document).ready(function () {
    $("#user-form-dilaco").validate({
        rules: {
            name: {
                required: true,
                minlength: 3,
                maxlength: 50
            },
            last_name: {
                minlength: 3,
                maxlength: 50
            },
            "Input.Mobile": {
                minlength: 6,
                maxlength: 20,
                phoneDilaco: true
            },
            "Input.Phone": {
                minlength: 6,
                maxlength: 20,
                phoneDilaco: true
            },
            e_mail: {
                email: true,
                maxlength: 80
            },
            position: {
                maxlength: 40
            },
            skypee: {
                maxlength: 80
            },
            main_language: {
                maxlength: 20
            },
            spoken_languages: {
                maxlength: 128
            },
            comment: {
                maxlength: 1500
            }
        },
        errorElement: "div",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
        }
    });

    $("#login_form").validate({
        rules: {
            "Input.Email": {
                required: true,
                minlength: 6,
                maxlength: 50
            },
            "Input.Password": {
                required: true,
                minlength: 6,
                maxlength: 50
            }
        },
        errorElement: "div",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
        }
    });

    $("#registration_form").validate({
        rules: {
            "Input.Email": {
                required: true,
                minlength: 6,
                maxlength: 50
            },
            "Input.Password": {
                required: true,
                minlength: 6,
                maxlength: 50
            },
            "Input.Company": {
                required: true,
                minlength: 3,
                maxlength: 50
            },
            "Input.TermsPrivacy": {
                required: true
            }
        },
        errorElement: "div",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent().parent());
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            if ($(element).prop("type") === "checkbox") {
                $(element).next().addClass("is-invalid");
            } else {
                $(element).addClass("is-invalid");
            }
            
        },
        unhighlight: function (element, errorClass, validClass) {
            
            if ($(element).prop("type") === "checkbox") {
                $(element).next().removeClass("is-invalid");
            } else {
                $(element).removeClass("is-invalid");
            }

        }
    });

});

jQuery.validator.addMethod("phoneDilaco", function (value, element) {
    return this.optional(element) || /(\d+\s?)*/.test(value);
}, "Csak számokat tartalmazhat");