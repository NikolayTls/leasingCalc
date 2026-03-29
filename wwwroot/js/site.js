$(document).ready(function () {
    $('#btnCalculate').click(function () {
        var loanRaw = $('#loanAmount').val();
        var monthsRaw = $('#months').val();
        var interestRaw = $('#interest').val();

        var loanAmount = parseFloat(loanRaw);
        var months = parseInt(monthsRaw);
        var interest = parseFloat(interestRaw);

        $('.error-msg').hide();
        $('input').removeClass('input-error');

        var isValid = true;
        
        if (!loanRaw || isNaN(loanAmount) || loanAmount <= 0) {
            $('#err-loanAmount').text('Задължително поле ( > 0 )').show();
            $('#loanAmount').addClass('input-error');
            isValid = false;
        }
        if (!monthsRaw || isNaN(months) || months <= 0) {
            $('#err-months').text('Задължително поле ( > 0 )').show();
            $('#months').addClass('input-error');
            isValid = false;
        }
        if (!interestRaw || isNaN(interest) || interest < 0) {
            $('#err-interest').text('Задължително поле').show();
            $('#interest').addClass('input-error');
            isValid = false;
        }

        if (!isValid) {
            return;
        }

        // AJAX request to server
        $.ajax({
            url: '/Home/CalculateCredit', // HomeController action
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                LoanAmount: loanAmount,
                Months: months,
                InterestRate: interest
            }),
            success: function (result) {
                // result is your CreditCalculatorResult object
                $('#monthly').text(result.monthlyPayment.toFixed(2));
                $('#total').text(result.totalPaid.toFixed(2));
                $('#interestTotal').text(result.interest.toFixed(2));
                $('#apr').text(result.apr.toFixed(2));

                $('#result').fadeIn();
            },
            error: function (xhr, status, error) {
                alert('Възникна грешка при изчислението: ' + error);
            }
        });
    });
});