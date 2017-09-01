angular.module("app.controller", [])
    .controller("PejabatController", function ($scope, PejabatService) {
        $scope.TambahTitle = "Tambah Pejabat";
        $scope.IsNew = true;
        $scope.Pejabats = [];
        $scope.Levels = ["Kelurahan", "RW", "RT"];
        $scope.JenisJabatans = ["Ketua", "Sekretaris"];

        PejabatService.source().then(function (data) {
            try {
                $scope.Pejabats = data;
            } catch (e) {
                alert(e.Message);
            }
        });

        $scope.Tambah = function (model)
        {
            model.Id = 0;
            model.Password = "Pejabat#1";
            model.ConfirmPassword = "Pejabat#1";
            model.InstansiID = 0;
            model.Status = true;
            model.userId = null;
            modelInstansi = null;

            PejabatService.Tambah(model).then(function (response) {
               
            });
        }

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