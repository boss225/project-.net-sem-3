app.controller("homeCtrl", function ($scope, $http, $rootScope) {
    $scope.$emit('LOAD');

    $scope.random = function () {
        return 0.5 - Math.random();
    }

    function makeRandom(inputArray) {
        angular.forEach(inputArray, function (value) {
            value.random = Math.random();
        });
        return inputArray;
    }

    $http({method: 'GET', url: $scope.urlCategories + 'GetCategoryLevel?level=1'
    }).then(function mySucces(response) {
        $scope.lv1category = response.data;
    }, function myError(response) {
        $scope.lv1category = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlCategories + 'GetCategoryLevel?level=2'
    }).then(function mySucces(response) {
        $scope.lv2category = response.data;
    }, function myError(response) {
        $scope.lv2category = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlSlideImages + 'GetSlideImages'
    }).then(function mySucces(response) {
        $scope.imgSlide = response.data;
    }, function myError(response) {
        $scope.imgSlide = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlProducts + 'GetProductsHot'
    }).then(function mySucces(response) {
        $scope.hotProds = makeRandom(response.data);
    }, function myError(response) {
        $scope.hotProds = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlProducts + 'GetProductsbyCategoryId/16'
    }).then(function mySucces(response) {
        $scope.prodCategory1 = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.prodCategory1 = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlProducts + 'GetProductsbyCategoryId/17'
    }).then(function mySucces(response) {
        $scope.prodCategory2 = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.prodCategory2 = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlProducts + 'GetProductsNew'
    }).then(function mySucces(response) {
        $scope.prodNew = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.prodNew = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlProducts + 'GetProductsPromote'
    }).then(function mySucces(response) {
        $scope.prodPromote = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.prodPromote = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlNewsServices + 'GetNewsServices'
    }).then(function mySucces(response) {
        $scope.news = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.news = response.statusText;
    });

});