function AjaxPost(url, data, cache, processData, dataType, contentType, successCallBack) {
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        processData: processData,
        cache: cache,
        dataType: dataType,
        contentType: contentType,
        success: successCallBack
    })
}

function AjaxGet(url, cache, processData, dataType, contentType, successCallBack) {
    $.ajax({
        url: url,
        type: "GET",
        processData: processData,
        cache: cache,
        dataType: dataType,
        contentType: contentType,
        success: successCallBack
    })
}