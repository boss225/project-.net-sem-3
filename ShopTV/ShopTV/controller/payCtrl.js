app.controller("payCtrl", function ($scope, $http, $location, $rootScope, ngCart, $filter, Notification) {

    $scope.radio = true;
    $scope.choose = 1;
    $scope.fullname = "";
    $scope.email = "";
    $scope.phone = "";
    $scope.address = "";

    //    angular.forEach(ngCart.getCart().items, function (value, key) {
    //        console.log($filter('currency')(value._price, "đ", 0));
    //    });

    $scope.orderProds = function () {

        if ($scope.radio === true) {
            $scope.fullname1 = $scope.fullname;
            $scope.email1 = $scope.email;
            $scope.phone1 = $scope.phone;
            $scope.address1 = $scope.address;
        }

        $scope.theTime = "TV" + Math.round(new Date().getTime() / 1000);
        //$scope.createdate = new Date();
        var d = new Date();
        var curr_date = d.getDate();
        var curr_month = d.getMonth()+1;
        var curr_year = d.getFullYear();
        var curr_hours = d.getHours();
        var curr_minutes = d.getMinutes();
        $scope.createdate = curr_year + "-" + curr_month + "-" + curr_date + " " + curr_hours + ":" + curr_minutes;

        if ($scope.email === "" || $scope.fullname === "" || $scope.phone === "" || $scope.address === "") {
            Notification.error({ message: 'Lỗi : Bạn chưa nhập đầy đủ thông tin !', positionY: 'top', positionX: 'right' });
        } else {
            $scope.orderdetails = [];
            angular.forEach(ngCart.getCart().items, function (value, key) {
                var originalprice = (value._price / (1 - value._seles / 100));
                var prices = (value._price * value._quantity);
                //nameseo là id ,imgname là size khi addtocart 
                $scope.orderdetails.push({
                    "OrderCode": $scope.theTime,
                    "ProductId": value._nameseo,
                    "ProductName": value._name+" - size:"+ value._imgname,
                    "ImageUrl": value._data.ImageUrl,
                    "Quantity": value._quantity,
                    "Price": originalprice,
                    "Discount": value._seles,
                    "ReducedPrice": prices
                });
            });

            var data = {
                "Code": $scope.theTime,
                "CreatedDate": $scope.createdate,
                "ContactName": $scope.fullname,
                "ContactAddress": $scope.address,
                "ContactPhone": $scope.phone,
                "ContactEmail": $scope.email,
                "ContactReceiverName": $scope.fullname1,
                "ContactReceiverAddress": $scope.address1,
                "ContactReceiverPhone": $scope.phone1,
                "ContactReceiverEmail": $scope.email1,
                "Note": "",
                "TotalPrice": ngCart.totalCost(),
                "OrderDetails": $scope.orderdetails
            };

            $http.post(
            "https://localhost:44374/api/ApiOrders/PostOrder",
            JSON.stringify(data),
                {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
            ).then(function mySucces(data) {
                $scope.orderproducts = data;

                $rootScope.$broadcast('ngCart:change', {});
                ngCart.$cart.items = [];
                localStorage.removeItem('cart');
                Notification.success({ message: 'Thông tin đơn hàng đã gởi thành công .\n\Chúng tôi sẽ liên hệ trong thời gian sớm nhất !', positionY: 'top', positionX: 'right' });
                $location.path('/');

            }, function myError(data) {
                Notification.error({ message: 'Lỗi : Mời bạn tải lại trang và nhập lại !', positionY: 'top', positionX: 'right' });
            });


            //            console.log($rootScope.infoFooter);
            //            ngCart.getTotalItems()

        }
    };

});
