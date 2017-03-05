app.controller("prodCtrl", function ($scope, $http, $routeParams) {
    $scope.param3 = $routeParams.id1;
    $scope.currentPage = 1;
    $scope.pageSize = 12;
    
    $scope.$emit('LOAD');

    $scope.random = function () {
        return 0.5 - Math.random();
    };

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

    $http({method: 'GET', url: $scope.urlProducts + 'GetProducts'
    }).then(function mySucces(response) {
        $scope.allproduct = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.allproduct = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlProducts + 'GetProductsHot'
    }).then(function mySucces(response) {
        $scope.hotProds = makeRandom(response.data);
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.hotProds = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlProducts + 'GetProductsbyCategoryId/' + $scope.param3
    }).then(function mySucces(response) {
        $scope.deltailcato = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.deltailcato = response.statusText;
    });

});