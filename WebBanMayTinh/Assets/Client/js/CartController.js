var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });

        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });

        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });

        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });

        $('.txtQuantity').on('change', function () {
            var CartItem = [
                {
                    Quantity: $(this).val(), //lấy giá trị hiện tại của textbox đó
                    Product: {
                        ID: $(this).data('id')
                    }
                }
            ];
            if ($(this).val() > 5 || $(this).val() < 1) {
                alert('Bạn không đươc đặt quá 5 sản phẩm');
                return;
            } else {
                $.ajax({
                    url: '/Cart/Update',
                    dataType: 'json',
                    data: { cartModel: JSON.stringify(CartItem) }, //chuyển mảng thành một chuỗi,  
                    type: 'POST',
                    success: function (response) {
                        if (response.status == true) {
                            window.location.href = '/gio-hang';
                        }
                    }
                });
            }
        });
    }
}
cart.init();