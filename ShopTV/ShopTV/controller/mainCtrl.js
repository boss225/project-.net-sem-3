app.controller("mainCtrl", function ($scope, $rootScope, $http, $timeout, $uibModal, Notification) {
    $scope.namecatalog = "Sản phẩm";

    $scope.urlCategories = "https://localhost:44374/api/ApiCategories/";
    $scope.urlProducts = "https://localhost:44374/api/ApiProducts/";
    $scope.urlAbout = "https://localhost:44374/api/ApiAboutShops/";
    $scope.urlSlideImages = "https://localhost:44374/api/ApiSlideImages/";
    $scope.urlComments = "https://localhost:44374/api/ApiUserComments/";
    $scope.urlContacts = "https://localhost:44374/api/ApiContacts/";
    $scope.urlNewsServices = "https://localhost:44374/api/ApiNewsServices/";
    $scope.urlImage = "https://localhost:44374/";

    // modal add to cart :begin
    $rootScope.open = function (size, id) {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'partials/myModalContent.html',
            controller: 'addtocartCtrl',
            size: size,
            resolve: {
                items: function () {
                    return id;
                }
            }
        });

    };
    // end
    $rootScope.newslimit = function (item) {
        return String(item).replace(/<[^>]+>/gm, ' ');
    }

    $scope.selectedIndex = 0;
    $scope.changeIndex = function (num) {
        $scope.selectedIndex = num;
    };

    $scope.changename = function (name) {
        $scope.namecatalog = name;
    };

    $scope.data = {
        availableOptions: [
            {name: 'Mặc định', arrange: '', boolean: ''},
            { name: 'Giá : Từ thấp tới cao', arrange: '(1 - Discount / 100)*Price', boolean: false },
            { name: 'Giá : Từ cao tới thấp', arrange: '(1 - Discount / 100)*Price', boolean: true },
            {name: 'Tên sản phẩm : A > Z', arrange: 'Name', boolean: false},
            {name: 'Tên sản phẩm : Z > A', arrange: 'Name', boolean: true}
        ],
        selectedOption: {name: 'Mặc định', arrange: '', boolean: ''} //This sets the default value of the select in the ui
    };

    $scope.$on('LOAD', function () {
        $scope.loading = true;
    });
    $scope.$on('UNLOAD', function () {
        $scope.loading = false;
    });

    $http({method: 'GET', url: $scope.urlAbout + 'GetAboutShop/1'
    }).then(function mySucces(response) {
        $scope.about = response.data;
    }, function myError(response) {
        $scope.about = response.statusText;
    });


}).directive("owlCarousel", function () {
    return {
        restrict: 'E',
        transclude: false,
        link: function (scope) {
            scope.initCarousel = function (element) {
                // provide any default options you want
                var defaultOptions = {
                };
                var customOptions = scope.$eval($(element).attr('data-options'));
                // combine the two options objects
                for (var key in customOptions) {
                    defaultOptions[key] = customOptions[key];
                }
                // init carousel
                $(element).owlCarousel(defaultOptions);
            };
        }
    };
}).directive('owlCarouselItem', [function () {
        return {
            restrict: 'A',
            transclude: false,
            link: function (scope, element) {
                // wait for the last item in the ng-repeat then call init
                if (scope.$last) {
                    scope.initCarousel(element.parent());
                }
            }
        };
    }])
.directive('fbCommentBox', function () {
    function createHTML(href, numposts, colorscheme, width) {
        return '<div class="fb-comments" ' +
                'data-href="' + href + '" ' +
                'data-numposts="' + numposts + '" ' +
                'data-colorsheme="' + colorscheme + '" ' +
                'data-width="' + width + '">' +
                '</div>';
    }

    return {
        restrict: 'A',
        scope: {},
        link: function postLink(scope, elem, attrs) {
            attrs.$observe('pageHref', function (newValue) {
                var href = newValue;
                var numposts = attrs.numposts || 5;
                var colorscheme = attrs.colorscheme || 'light';
                var width = attrs.width || '100%';
                elem.html(createHTML(href, numposts, colorscheme, width));
                FB.XFBML.parse(elem[0]);
            });
        }
    };
});