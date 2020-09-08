(function ($) {

    $("#wizard1").steps({
        headerTag: "h3",
        bodyTag: "fieldset"
    });

    var form = $("#wizard2").show();

    form.steps({
        headerTag: "h3",
        bodyTag: "fieldset",
        onStepChanging: function (event, currentIndex, newIndex) {
            // Allways allow previous action even if the current form is not valid!
            if (currentIndex > newIndex) {
                return true;
            }
            // Forbid next action on "Warning" step if the user is to young
            if (newIndex === 3 && Number($("#age-2").val()) < 18) {
                return false;
            }
            // Needed in some cases if the user went back (clean up)
            if (currentIndex < newIndex) {
                // To remove error styles
                form.find(".body:eq(" + newIndex + ") label.error").remove();
                form.find(".body:eq(" + newIndex + ") .error").removeClass("error");
            }
            form.validate().settings.ignore = ":disabled,:hidden";
            return form.valid();
        },
        onFinishing: function (event, currentIndex) {
            form.validate().settings.ignore = ":disabled";
            return form.valid();
        },
        onFinished: function (event, currentIndex) {
            swal({
                title: "Well done!",
                text: "You successfully completed the form wizard.",
                type: "success"
            });
        }
    }).validate({
        rules: {
            'userName': {
                required: true
            },
            'password': {
                required: true,
                minlength: 1
            },
            'confirmPassword': {
                required: true,
                minlength: 1,
                equalTo: "#password"
            },
            'firstName': {
                required: true
            },
            'lastName': {
                required: true
            },
            'gender': {
                required: true
            },
            'email': {
                required: true,
                email: true
            },
            'subscriptionPlan': {
                required: true
            },
            'acceptTerms': {
                required: true
            }
        },
        messages: {
            'acceptTerms': {
                required: 'You need to agree with our terms and conditions.'
            }
        }
    });

})(jQuery);