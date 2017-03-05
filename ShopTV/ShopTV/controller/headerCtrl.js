app.controller("headerCtrl", function ($scope, $http) {

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

    $http({method: 'GET', url: $scope.urlContacts + 'GetContacts'
    }).then(function mySucces(response) {
        $scope.contacts = response.data;
    }, function myError(response) {
        $scope.contacts = response.statusText;
    });
});