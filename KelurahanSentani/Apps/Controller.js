angular.module("app.controller", [])
    .controller("PejabatController", function ($scope, PejabatService) {
        $scope.TambahTitle = "Tambah Pejabat";
        $scope.IsNew = true;
        $scope.Pejabats = [];
        PejabatService.source().then(function (response) {
            try {
                $scope.Pejabats = response.data;
            } catch (e) {
                alert(e.Message);
            }
        });

    })


    .controller("StrukturKelurahanController", function ($scope, StrukturKelurahanService,PejabatService) {
        $scope.Strukturs = [];
        $scope.Pejabats = [];
        PejabatService.source().then(
            function (response) {
                $scope.Pejabats = response.data;
                StrukturKelurahanService.source().then(function (response) {
                    $scope.Strukturs = response.data;

                });
            }
        );
     

    })
    ;