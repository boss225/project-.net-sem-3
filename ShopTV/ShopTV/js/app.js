var app = angular.module('myApp', [
    'ngRoute', "ngMaterial", "ui.bootstrap", "ngAnimate",
    "ngMessages", 'ngCookies', 'ngResource', 'ngCart', 'ngSanitize', 'ui-notification','angularUtils.directives.dirPagination','sticky'
]);

app.config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        $locationProvider.html5Mode(true).hashPrefix('!');
        var templateUrl = 'partials/';
        $routeProvider.
                when('/', {
                    templateUrl: templateUrl + 'home.html',
                    controller: 'homeCtrl',
                    title: "Trang chủ - TV Shop"
                }).
                when('/gioi-thieu', {
                    templateUrl: templateUrl + 'about.html',
                    title: "Giới Thiệu"
                }).
                when('/lien-he', {
                    templateUrl: templateUrl + 'contact.html',
                    title: "Liên hệ"
                }).
                when('/chinh-sach-cua-hang', {
                    templateUrl: templateUrl + 'privacy-policy.html',
                    controller: 'policyCtrl',
                    title: "Chính sách cửa hàng"
                }).
                when('/gio-hang', {
                    templateUrl: templateUrl + 'shopping-cart.html',
                    controller: 'homeCtrl',
                    title: "Giỏ hàng "
                }).
                when('/thanh-toan', {
                    templateUrl: templateUrl + 'paypal.html',
                    controller: 'payCtrl',
                    title: "Thanh toán "
                }).
                when('/tat-ca-san-pham', {
                    templateUrl: templateUrl + 'allproduct.html',
                    controller: 'prodCtrl',
                    title: "Tất cả sản phẩm "
                }).
                when('/danh-muc/:name-:id1.:lv', {
                    templateUrl: templateUrl + 'products.html',
                    controller: 'prodCtrl',
                    title: "Chi tiết danh mục sản phẩm "
                }).
                when('/tim-kiem/:key', {
                    templateUrl: templateUrl + 'searchproduct.html',
                    controller: 'searchprodCtrl',
                    title: "Tìm kiếm "
                }).
                when('/chi-tiet/:name-:id', {
                    templateUrl: templateUrl + 'detailproducts.html',
                    controller: 'detailCtrl',
                    title: "Chi tiết sản phẩm"
                }).
                when('/admin', {
                    templateUrl: templateUrl + 'admin.html',
                    title: "Quản lý trang bán hàng"
                }).
                when('/tin-tuc-blog', {
                    templateUrl: templateUrl + 'news.html',
                    controller: 'newsCtrl',
                    title: "Tin tức - Blog"
                }).
                when('/tin-tuc-blog/:id', {
                    templateUrl: templateUrl + 'newsdetail.html',
                    controller: 'newsCtrl',
                    title: "Tin tức - Blog"
                }).
                otherwise({
                    templateUrl: templateUrl + 'error404.html',
                    title: "Không tìm thấy "
                });

    }]).run(function ($rootScope, $route) {
        $rootScope.$on("$routeChangeSuccess", function (currentRoute, previousRoute) {
            $rootScope.title = $route.current.title;
        });
    });
