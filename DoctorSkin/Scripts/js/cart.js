let numItemCart = document.querySelector('.aa-cart-notify')
let iduser = null;
const nameuser = document.querySelector('#nameuser');
if (nameuser.hasAttribute('data-id')) {
    iduser = nameuser.dataset.id;
}

//lấy danh sách giỏ hàng lúc hover
$.ajax({
    type: "GET",
    dataType: "json",
    url: "https://localhost:44307/api/getCart/"+iduser,
    headers: {
        // 'Content-Type': 'application/json',
        'Content-Type': 'application/x-www-form-urlencoded',
        'Accept': 'application/json'
    },
    success: function (res) {
        let htmlcart = res.slice(0,4).map(item => {
            return `<li>
              <div><a class="aa-cartbox-img" href="#"><img src="${item.imgp}" alt="img"></a></div>
              <div class="aa-cartbox-info" style="display:flex;justify-content:space-between;width:75%">
                <div><a href="#" style="-webkit-line-clamp: 1; -webkit-box-orient: vertical; overflow: hidden; display: -webkit-box;font-size:14px">${item.namep}</a></div>
                <p class="money" style="font-size:14px;color:#ee4d2d" value="${item.pricep}">${item.pricep}</p>
              </div>
             <div> <a class="aa-remove-product" href="#" style="top:0 !important"><span class="fa fa-times"></span></a></div>
            </li>`
        })
        document.querySelector('#list-product-cart').innerHTML = htmlcart.join('')
    },
    error: function (err) {
        console.log("Lỗi Api")
    }
})

//thêm vào giỏ hàng
function addCart(idp)
{
    if (iduser == null) {
        window.location.href = "/dang-nhap"
    }
    else {
        let data = {
            "iduser": iduser,
            "idp": idp,
            "quanlity": 1
        }
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "https://localhost:44307/api/Carts",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            data: JSON.stringify(data),
            success: function (res) {
                swal({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Thêm sản phẩm thành công',
                    showConfirmButton: false,
                    timer: 1000
                }).then(() => {
                    numItemCart.innerText = Number(numItemCart.innerText) + 1
                })
            },
            error: function (err) {
                console.log("Lỗi Api")
            }
        })
    }
}

//lấy toàn bộ giỏ hàng
function toCart() {
    window.location.href = "/carts";
}
$.ajax({
    type: "GET",
    dataType: "json",
    url: "https://localhost:44307/api/getCart/" + iduser,
    headers: {
        // 'Content-Type': 'application/json',
        'Content-Type': 'application/x-www-form-urlencoded',
        'Accept': 'application/json'
    },
    success: function (res) {
        numItemCart.innerText = res.reduce((acc, curr) => acc + curr.quanlity, 0)
        //let htmlcart = res.slice(0, 4).map(item => {
        //    return `<div class="row" id="div-pr-${item.idp}">
        //                <div class="row" style="margin-bottom:15px">
        //                <div class="col-md-4" style="display: flex;justify-content: space-between;">
        //            <div>
        //              <input type="checkbox"></input>
        //            </div>
        //            <div class="" style="padding: 0 8px;">
        //              <a class="aa-cartbox-img" href="#"><img style="width: 100px;height: 100px;object-fit: cover;"
        //                  src="${item.imgp}"
        //                  alt="img"></a>
        //            </div>
        //            <div>
        //              <a href="#" style="-webkit-line-clamp: 2; -webkit-box-orient: vertical; overflow: hidden; display: -webkit-box;font-size:14px">${item.namep}</a>
        //              <img style="height: 18px;object-fit: contain;"
        //                src="https://cf.shopee.vn/file/vn-50009109-d73bf8c455170812e24c11d1852ca7b4" alt="">
        //            </div>
        //          </div>

        //          <div class="col-md-2">
        //            <div style="opacity: 0.7;">
        //              <div>Thương hiệu:</div>
        //              <div>${item.brandp}</div>
        //            </div>
        //          </div>
        //          <div class="col-md-2">
        //            <div>
        //              <p class="money" value="${item.pricep}">${item.pricep}</p>
        //            </div>
        //          </div>
        //          <div class="col-md-1">
        //            <div>
        //              <input style="width: 100%;" type="number" value="${item.quanlity}"></input>
        //            </div>
        //          </div>
        //          <div class="col-md-2">
        //            <div>
        //              <p class="money" value="${item.pricep * item.quanlity}" style="color: #ee4d2d">${item.pricep * item.quanlity}</p>
        //            </div>
        //          </div>
        //          <div class="col-md-1">
        //            <div>
        //              <a class="aa-remove-product" href="#" style="top:0 !important">
        //                <span class="fa fa-times" onclick="deletePro(${item.idp})"></span></a>
        //            </div>
        //          </div>
        //            </div>
        //            </div>`
        //})
        //document.querySelector('#table-data-cart').innerHTML = htmlcart.join('')
    },
    error: function (err) {
        console.log("Lỗi Api")
    }
})

//Xoa san pham
function deletePro(idp) {
    
    $.ajax({
        type: "delete",
        url: "https://localhost:44307/api/carts?idp="+idp+"&iduser="+iduser,
        success: function (res) {
            swal({
                position: 'top-end',
                icon: 'success',
                title: 'xóa sản phẩm thành công',
                showconfirmbutton: false,
                timer: 1500
            })
                .then(() => {
                    document.getElementById(`div-pr-${idp}`).remove()
                })
        },
        error: function(err)
        {
            console.log(iduser)
            console.log("lỗi");
        }
    })
}