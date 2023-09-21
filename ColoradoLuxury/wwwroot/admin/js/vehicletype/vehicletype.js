$(function () {
    $(document).on("keyup", "#permile", (event) => {
        let inputValue = event.target.value;

        // Replace commas with periods
        inputValue = inputValue.replace('.', ",");

        // Update the input value with the replaced string
        event.target.value = inputValue;


    })
})