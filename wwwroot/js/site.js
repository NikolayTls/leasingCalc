$(document).ready(function() {
    $('#btnCalculate').click(function() {
        var loanAmount = parseFloat($('#loanAmount').val());
        var months = parseInt($('#months').val());
        var interest = parseFloat($('#interest').val());

        if(isNaN(loanAmount) || isNaN(months) || isNaN(interest) || loanAmount <= 0 || months <= 0 || interest < 0){
            alert('Моля, въведете валидни стойности!');
            return;
        }

        // AJAX request to server
        $.ajax({
            url: '@Url.Action("CalculateCredit", "Home")', // HomeController action
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                LoanAmount: loanAmount,
                Months: months,
                InterestRate: interest
            }),
            success: function(result) {
                // result is your CreditCalculatorResult object
                $('#monthly').text(result.monthlyPayment.toFixed(2));
                $('#total').text(result.totalPaid.toFixed(2));
                $('#interestTotal').text(result.interest.toFixed(2));
                $('#apr').text(result.apr.toFixed(2));

                $('#result').fadeIn();
            },
            error: function(xhr, status, error) {
                alert('Възникна грешка при изчислението: ' + error);
            }
        });
    });
});