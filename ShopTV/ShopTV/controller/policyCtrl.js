app.controller("policyCtrl", function ($scope, $http) {
    $scope.$emit('LOAD');

    $http({method: 'GET', url: $scope.urlAbout + 'GetAboutShop/2'
    }).then(function mySucces(response) {
        $scope.regulations = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.regulations = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlAbout + 'GetAboutShop/3'
    }).then(function mySucces(response) {
        $scope.guarantee = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.guarantee = response.statusText;
    });

    $http({method: 'GET', url: $scope.urlAbout + 'GetAboutShop/4'
    }).then(function mySucces(response) {
        $scope.delivery = response.data;
        $scope.$emit('UNLOAD');
    }, function myError(response) {
        $scope.delivery = response.statusText;
    });

});
