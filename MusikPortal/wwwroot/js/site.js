// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

<script>
    $(document).ready(function ()
    {

        $("#login").change(function (e) {
            let log = e.target.value;
            let r = true;
            $.getJSON('/Login/IsLoginInUse', function (data) {
                $.each(data, function (key, val) {
                    if (log == val) {
                        $("#log").css("display", "block");
                        r = false;
                    }
                    if (r) {
                        $("#log").css("display", "none");
                    }
                })


            });


        })
    }

    $(document).ready(function ()
    {
    $('#emailInput').on('blur', function () {
            var email = $(this).val();
    $.get('/Login/IsEmailInUse', {email: email }, function (data) {
                if (data === true) {
        $('#emailStatus').text('Email уже занят.');
                }

            });
        })};);
    );)
</script>