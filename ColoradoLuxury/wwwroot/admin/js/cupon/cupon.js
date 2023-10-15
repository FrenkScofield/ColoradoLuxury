

function CreateCupon() {
    let validationCount = 0;
    let output = $("output ul");
    output.empty();
    let newcupon = $("#newCoupon").val();
    let percentage = $("#percentage").val();
    let amount = $("#amount").val();

    let usableCount = $("#usable-count").val();

    let cuponDeathline = $("#inputdate").val();

    if (cuponDeathline == '') {
        cuponDeathline = '0001-01-01'
    }

    let parseUsableCount = ParseFromStringToInteger(usableCount);

    if (isNaN(parseUsableCount)) {
        let li = $("<li>");
        li.text("Data is not valid for Usable count flied!!");
        output.append(li);

        ++validationCount;
    }

    if (amount == '')
        amount = 0;
    if (percentage == '')
        percentage = 0;

    if (usableCount == '')
        usableCount = 0;

    if (validationCount > 0) {
        return;
    }

    let data = {
        Id: 0,
        NewCupon: newcupon,
        Percentage: percentage,
        Amount: amount,
        CouponDeatline: cuponDeathline,
        UsableCount: usableCount,

        Status: 1
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

        if (response.hasOwnProperty('badDataForCuponUseageCount')) {
            let li = $("<li>");
            li.text("Used count is not grater than Usable count!!");
            output.append(li);

            return;
        }

        if (response.hasOwnProperty('unexpectedCuponDeathline')) {
            let li = $("<li>");
            li.text("Data is not valid for Cupon deathline flied!!");
            output.append(li);

            return;
        }

        if (response.hasOwnProperty('unexpectedUsableCount')) {
            let li = $("<li>");
            li.text("Data is not valid for Usable count flied!!");
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