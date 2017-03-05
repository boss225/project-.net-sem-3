app.controller("detailCtrl", function ($http, $scope, $rootScope, $routeParams, $uibModal) {
    $scope.param1 = $routeParams.name;
    $scope.param2 = $routeParams.id;
    $scope.param3 = $routeParams.id1;
    $scope.zoomLvl = 4;
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

    // push value size to the array :begin
    var formatSizeString = function (input) {
        //Validate the input
        if (!input) {
            return '';
        }
        var AddArray = [];
        var inputArray = input.split(' ');
        for (var i = 0; i < inputArray.length; i++) {
            var adminTimeArray = inputArray[i].split(',');
            AddArray.push({ 'size': adminTimeArray[0] });
        }
        return AddArray;
    };
    // end

    $http({
        method: 'GET', url: $scope.urlProducts + 'GetProduct/' + $scope.param2
    }).then(function mySucces(response) {
        $scope.detailprods = response.data;
        $scope.descriplimit = String($scope.detailprods.Description).replace(/<[^>]+>/gm, ' ');

        $scope.sizeformat = formatSizeString($scope.detailprods.Size);
        $scope.size = $scope.sizeformat[0].size;
        $scope.changesize = function (size) {
            $scope.size = size;
        };

        $scope.imgP1 = $scope.detailprods.ImageUrl;
        $scope.imgP0 = $scope.imgP1;
        $scope.changeimg = function (key) {
            $scope.imgP0 = key;
        };

    }, function myError(response) {
        $scope.detailprods = response.statusText;
    });

    $http({
        method: 'GET', url: $scope.urlProducts + 'GetSimilartProducts/' + $scope.param2
    }).then(function mySucces(response) {
        $scope.relatedprods = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.relatedprods = response.statusText;
    });

}).controller('addtocartCtrl', function ($uibModalInstance, $http, $scope, items) {
    $scope.$emit('LOAD');

    // push value size to the array :begin
    var formatSizeString = function (input) {
        //Validate the input
        if (!input) {
            return '';
        }
        var AddArray = [];
        var inputArray = input.split(' ');
        for (var i = 0; i < inputArray.length; i++) {
            var adminTimeArray = inputArray[i].split(',');
            AddArray.push({ 'size': adminTimeArray[0] });
        }
        return AddArray;
    };
    // end

    $http({
        method: 'GET', url: 'https://localhost:44374/api/ApiProducts/GetProduct/' + items
    }).then(function mySucces(response) {        
        $scope.modeladdcart = response.data;
        $scope.$emit('UNLOAD');
        $scope.sizeformat = formatSizeString($scope.modeladdcart.Size);
        $scope.size = $scope.sizeformat[0].size;
        $scope.changesize = function (size) {
            $scope.size = size;
        };

    }, function myError(response) {
        $scope.modeladdcart = response.statusText;
    });

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

}).controller("searchprodCtrl", function ($http, $scope, $routeParams, $location) {
    $scope.searchkey = $routeParams.key;
    $scope.param3 = $routeParams.id1;
    $scope.currentPage = 1;
    $scope.pageSize = 12;

    $scope.$emit('LOAD');
    $scope.submit = function () {
        if ($scope.keysearchs) {
            $location.path('/tim-kiem/' + $scope.keysearchs);
            $scope.keysearchs = "";
        }
    };

    $scope.random = function () {
        return 0.5 - Math.random();
    };

    function makeRandom(inputArray) {
        angular.forEach(inputArray, function (value) {
            value.random = Math.random();
        });
        return inputArray;
    }

    $http({
        method: 'GET', url: $scope.urlProducts + 'GetProducts?name=' + $scope.searchkey
    }).then(function mySucces(response) {
        $scope.searchprod = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.searchprod = response.statusText;
    });

    $http({
        method: 'GET', url: $scope.urlCategories + 'GetCategoryLevel?level=1'
    }).then(function mySucces(response) {
        $scope.lv1category = response.data;
    }, function myError(response) {
        $scope.lv1category = response.statusText;
    });

    $http({
        method: 'GET', url: $scope.urlCategories + 'GetCategoryLevel?level=2'
    }).then(function mySucces(response) {
        $scope.lv2category = response.data;
    }, function myError(response) {
        $scope.lv2category = response.statusText;
    });

    $http({
        method: 'GET', url: $scope.urlProducts + 'GetProducts'
    }).then(function mySucces(response) {
        $scope.allproduct = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.allproduct = response.statusText;
    });

    $http({
        method: 'GET', url: $scope.urlProducts + 'GetProductsHot'
    }).then(function mySucces(response) {
        $scope.hotProds = makeRandom(response.data);
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.hotProds = response.statusText;
    });

    $http({
        method: 'GET', url: $scope.urlProducts + 'GetProductsbyCategoryId/' + $scope.param3
    }).then(function mySucces(response) {
        $scope.deltailcato = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.deltailcato = response.statusText;
    });

});

