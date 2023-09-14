

function CreateCupon() {
    let newcupon = $("#newCoupon").val();
    let percentage = $("#percentage").val();
    let amount = $("#amount").val();
    let cuponcode = $("#couponCode").val();
    let cuponDeatline = $("#inputdate").val();

    if (amount == '')
        amount = 0;
    if (percentage == '')
        percentage = 0;

    let data = {
        Id: 0,
        NewCupon: newcupon,
        Percentage: percentage,
        Amount: amount,
        CuponCode: cuponcode,
        CuponDeatline: cuponDeatline,
        Status: true
    }

    AjaxPost("/WebCms/RandomCoupon/AddCoupon/", JSON.stringify(data), true, true, "json", "application/json; charset=utf-8", (response) => {

        console.log(response);
        let output = $("output ul");
        output.empty();

        if (response.hasOwnProperty('modelIsEmpty') || response.hasOwnProperty('discount')) {
            let li = $("<li>");
            li.text("Data can't be empty!");
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