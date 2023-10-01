$(function () {
    ReplaceDotInteadComma("#mile");
    ReplaceDotInteadComma("#minimumBookingvalueForDistance");
    ReplaceDotInteadComma("#minimumBookingvalueForHourly");
    ReplaceESymbolInteadEmpty("#minimumDuration");



})


function ReplaceDotInteadComma(id) {
    $(document).on("keyup", id, (event) => {
        let inputValue = event.target.value;

        // Replace commas with periods
        inputValue = inputValue.replace('.', ",");

        // Update the input value with the replaced string
        event.target.value = inputValue;


    })


    
}

function ReplaceESymbolInteadEmpty(id) {
    $(document).on("keyup", id, (event) => {
        let inputValue = event.target.value;

        // Replace commas with periods
        inputValue = inputValue.replace('e', "");

        // Update the input value with the replaced string
        event.target.value = inputValue;


    })
}