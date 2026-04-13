$(document).ready(function () {
    $('#btnCalculate').click(function () {
        var itemPriceRaw = $('#itemPrice').val();
        var downPaymentRaw = $('#downPayment').val();
        var monthsRaw = $('#months').val();
        var monthlyInstallmentRaw = $('#monthlyInstallment').val();
        var processingFeePercentageRaw = $('#processingFeePercentage').val();

        var itemPrice = parseFloat(itemPriceRaw);
        var downPayment = parseFloat(downPaymentRaw);
        var monthlyInstallment = parseFloat(monthlyInstallmentRaw);
        var processingFeePercentage = parseFloat(processingFeePercentageRaw);

        $('.error-msg').hide();
        $('input').removeClass('input-error');

        var isValid = true;
        
        if (!itemPriceRaw || isNaN(itemPrice) || itemPrice <= 0) {
            $('#err-itemPrice').text('Задължително поле ( > 0 )').show();
            $('#itemPrice').addClass('input-error');
            isValid = false;
        }
        
        if (!downPaymentRaw || isNaN(downPayment) || downPayment < 0) {
            $('#err-downPayment').text('Задължително поле ( >= 0 )').show();
            $('#downPayment').addClass('input-error');
            isValid = false;
        }

        // Validate months specifically as integer
        var isInteger = /^\d+$/.test(monthsRaw);
        var months = parseInt(monthsRaw, 10);
        if (!monthsRaw || !isInteger || isNaN(months) || months <= 0) {
            $('#err-months').text('Задължително поле (цяло число > 0 )').show();
            $('#months').addClass('input-error');
            isValid = false;
        }

        if (!monthlyInstallmentRaw || isNaN(monthlyInstallment) || monthlyInstallment <= 0) {
            $('#err-monthlyInstallment').text('Задължително поле ( > 0 )').show();
            $('#monthlyInstallment').addClass('input-error');
            isValid = false;
        }

        if (!processingFeePercentageRaw || isNaN(processingFeePercentage) || processingFeePercentage < 0) {
            $('#err-processingFeePercentage').text('Задължително поле ( >= 0 )').show();
            $('#processingFeePercentage').addClass('input-error');
            isValid = false;
        }

        // Additional validation
        if (itemPrice > 0 && downPayment >= itemPrice) {
            $('#err-downPayment').text('Първоначалната вноска трябва да е по-малка от цената').show();
            $('#downPayment').addClass('input-error');
            isValid = false;
        }

        if (!isValid) {
            return;
        }

        // AJAX request to server
        $.ajax({
            url: '/Home/CalculateCredit',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                ItemPrice: itemPrice,
                DownPayment: downPayment,
                Months: months,
                MonthlyInstallment: monthlyInstallment,
                ProcessingFeePercentage: processingFeePercentage
            }),
            success: function (result) {
                $('#apr').text(result.apr.toFixed(2));
                $('#totalPaid').text(result.totalPaid.toFixed(2));
                $('#totalFees').text(result.totalFees.toFixed(2));

                $('#result').fadeIn();
            },
            error: function (xhr, status, error) {
                alert('Възникна грешка при изчислението: ' + error);
            }
        });
    });
});