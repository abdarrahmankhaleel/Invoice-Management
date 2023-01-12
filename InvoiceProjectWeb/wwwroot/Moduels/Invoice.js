let ddlProduct = document.getElementById("ddlProduct");
let quntity = document.getElementById("quntity");
let price = document.getElementById("price");
let discount = document.getElementById("discount");

$(document).ready(() => {
    ShowTable();
    GetAllTotal()
});

function SaveProductInInvoiceTemp() {
    alert("um in fb add");
    let ddlCategoryId = document.getElementById("ddlCategoryId");
    let objProduct = {
        categoryId: ddlCategoryId.value,
        productId: ddlProduct.value,
        qtyChoosed: quntity.value,
        priceOrigin: price.value,

    }
    let data = JSON.stringify(objProduct)
    $.ajax({
        url: '/api/Invoice/',
        method: 'POST',
        contentType: 'application/json',
        data: data,
        cache: false,
        success: (data) => {
            ShowTable();
            Reset()
            GetAllTotal()
        }

    });

}

function GetProductsByCategory(id) {
    alert(id)
    if (id == "")
        ddlProduct.innerHTML = `<option value="">اختر نوع الفئة</option>`

    else {
        $.ajax({
            url: `/api/Invoice/${id}`,
            method: 'GET',
            cache: false,
            success: (data) => {
                let Product = ""
                Product += `<option value="">اختر نوع المنتج</option>`
                for (var i in data) {
                    Product += `<option value="${data[i].id}">${data[i].name}</option>`
                }
                ddlProduct.innerHTML = Product
            }
        })
    }
    
}

function GetPriceProcuctQty(id) {
    alert(id)

        $.ajax({
            url: `/Home/GetPriceProduct/${id}`,
            method: 'GET',
            cache: false,
            success: (data) => {
                $("#quntity").val(data.quantity)
                $("#price").val(data.price)
            }
        })
    
} 


function ShowTable () {
    $.ajax({
        url: `/api/Invoice`,
        method: 'GET',
        cache: false,
        success: (data) => {
            let Table = ""
            for (var i in data) {
                Table +=`<tr>
                        <td>${data[i].category.name}</td>
                        <td>${data[i].product.name}</td>
                        <td>${data[i].priceOrigin}</td>
                        <td>${data[i].qtyOrigin}</td>
                        <td>${data[i].qtyChoosed}</td>
                        <td>${data[i].totalPrice}</td>
                        <td><a class="btn btn-danger" onclick="DeleteInvTemp(${data[i].id})" style="color:#ffff"><i class="fa fa-trash"></i></a></td>
                    </tr>`
            }

            $("#tablebody").html(Table)
        }
    })

} 

function Reset() {
    let ddlCategoryId = document.getElementById("ddlCategoryId");
 
       ddlCategoryId.value=""
      ddlProduct.value=""
       quntity.value=0
      price.value=0

    }

DeleteInvTemp = (id) =>
{
    $.ajax({
        url: `/api/Invoice/${id}`,
        method: 'DELETE',
        cache: false,
        success: (data) => {
            ShowTable();
        }

    });
}


function GetAllTotal() {

    $.ajax({
        url: `/Home/GetAllTotalPrice`,
        method: 'GET',
        cache: false,
        success: (data) => {
            $("#allTotal").val(data)
            $("#afterDescount").val(data) 
        }
    })

}

function AfterDescount()
{
    $("#afterDescount").val($("#allTotal").val() - $("#discount").val())
    
}

discount.addEventListener("change", AfterDescount)
discount.addEventListener("keyup", AfterDescount)



