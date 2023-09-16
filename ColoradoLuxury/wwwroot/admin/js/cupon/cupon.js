

function CreateCupon() {
    let newcupon = $("#newCoupon").val();
    let percentage = $("#percentage").val();
    let amount = $("#amount").val();

    let cuponDeatline = $("#inputdate").val();

    if (cuponDeatline == '') {
        cuponDeatline = '0001-01-01'
    }

    if (amount == '')
        amount = 0;
    if (percentage == '')
        percentage = 0;

    let data = {
        Id: 0,
        NewCupon: newcupon,
        Percentage: percentage,
        Amount: amount,
        CouponDeatline: cuponDeatline,

        Status: true
    }

    AjaxPost("/WebCms/RandomCoupon/AddCoupon/", JSON.stringify(data), true, true, "json", "application/json; charset=utf-8", (response) => {

        console.log(response);
        let output = $("output ul");
        output.empty();

        if (response.hasOwnProperty('modelIsEmpty') || response.hasOwnProperty('discount')) {
            let li = $("<li>");
            li.text("Data can't be empty both percentage and amount!!");
            output.append(li);

            return;
        }

        if (response.hasOwnProperty('unexpectedDiscount')) {
            let li = $("<li>");
            li.text("Data can not fill both percentage and amount!");
            output.append(li);

            return;
        }
        
        if (response.hasOwnProperty('success')) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Data was successfully added',
            }).then((result) => {
                if (result.isConfirmed) {

                    location.href = "/webcms/RandomCoupon/Coupon"

                }
            });
        }



    })
}