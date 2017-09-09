angular.module("app.controller", [])
    .controller("PejabatController", function ($scope, PejabatService,$window) {
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

            PejabatService.Insert(model).then(function (response) {
               
            });
        }
        $scope.SaveEdit = function(model)
        {
            PejabatService.put(model, $scope.SelectedItem).then(function (response) {

            });
        }

        $scope.DeleteItem = function (item)
        {
            var deleteUser = $window.confirm("Anda Yakin Menghapus " + "'" + item.Nama + "'?");
            if (deleteUser) {
                PejabatService.delete(item).then(function (response) {

                });
            }
        }


        $scope.Edit = function(item)
        {
            $scope.SelectedItem = item;
            $scope.model = angular.copy(item);
           
        }
    })
    
    .controller("StrukturKelurahanController", function ($scope, StrukturKelurahanService,PejabatService,$window) {
        $scope.Strukturs = [];
        $scope.Pejabats = [];
        $scope.IsBusy = false;
        

        StrukturKelurahanService.source().then(function (response) {
            $scope.Strukturs = response.data;
            $scope.Pejabats = PejabatService.GetPejabatRW();
            
        });

        $scope.SetNoActive = function (item) {
            $scope.SelectedRW = item;
            $(document).ready(function () {
                $('.btn').click(function (e) {

                    var a = $('.btn').removeClass('active');


                });
            });



        };

        $scope.Save = function (item, SelectedPejabat)
        {
            try {
                $scope.IsBusy = true;
                if (item.Id === undefined) {
                    if (SelectedPejabat === undefined) {
                        alert("Tentukan Ketua RW");
                    } else {
                        item.Id = 0;
                        item.PejabatId = SelectedPejabat.Id;
                        item.Pejabat = SelectedPejabat;
                        StrukturKelurahanService.Insert(item).then(function (response) {

                            alert("Success");
                        });
                    }

                } else {

                }

            } catch (e) {
                alert(e.message);
            } finally
            {
                $scope.IsBusy = false;
            }
           
           
          
        }

        $scope.SelectRW = function (item) {
            $scope.SelectedRW = item;
        };

        $scope.SelectRT = function (item) {
            $scope.SelectedRT = item;
            $scope.model = angular.copy(item);
        };

        $scope.AddRT = function (rw, model) {
            StrukturKelurahanService.AddRT(rw, model).then(function (response) {


            });
        };

        $scope.SaveEditRT = function (item, selected) {
            StrukturKelurahanService.putRT(item, selected).then(function (response) { });

        };

        $scope.DeleteRT = function (item, daftar) {
            var deleteUser = $window.confirm("Anda Yakin Menghapus " + "' RT " + item.Nama + "'?");
            if (deleteUser) {
                StrukturKelurahanService.deleteRT(item, daftar).then(function (response) { });
            }
           
        };

    })

    .controller("KartuKeluargaController", function ($scope, StrukturKelurahanService, KartuKeluargaService, $window,Helpers,$rootScope) {
        $scope.Strukturs = [];
        $scope.IsBusy = false;
        $scope.Kepercayaan = Helpers.Kepercayaan();
        $scope.JenisKelamin = Helpers.JenisKelamin();
        $scope.KartuKeluarga = [];

        KartuKeluargaService.source().then(function (response) {
            $scope.KartuKeluarga = response;
            StrukturKelurahanService.source().then(function (response) {
                $scope.Strukturs = response.data;
            });
        });
      
        $scope.SimpanKK = function (data,penduduk) {
            try {
                $scope.IsBusy = true;
                data.Id = 0;
                data.RTId = data.RT.Id;
                data.DaftarKeluarga = [];
                penduduk.Id = 0;
                data.DaftarKeluarga.push(penduduk);
                KartuKeluargaService.Insert(data).then(function (response) { })
            } catch (e) {
                alert(e.message);
            } finally {
                $scope.IsBusy = false;
            }



        }

      

        $scope.SaveEditRT = function (item, selected) {
            StrukturKelurahanService.putRT(item, selected).then(function (response) { });

        };

        $scope.DeleteRT = function (item, daftar) {
            var deleteUser = $window.confirm("Anda Yakin Menghapus " + "' RT " + item.Nama + "'?");
            if (deleteUser) {
                StrukturKelurahanService.deleteRT(item, daftar).then(function (response) { });
            }

        };


        $scope.GotoDetails = function (kk)
        {
            $rootScope.SelectedKK = kk;
        }

    })

    .controller("KartuKeluargaDetailController", function ($scope, StrukturKelurahanService, KartuKeluargaService, $window, Helpers, $rootScope) {

        $scope.Strukturs = [];
        $scope.IsBusy = false;
        $scope.Kepercayaan = Helpers.Kepercayaan();
        $scope.JenisKelamin = Helpers.JenisKelamin();
        $scope.Kewarganegaraan = Helpers.Kewarganegaraan();
        $scope.Hubungan = Helpers.Hubungan();
        $scope.StatusPerkawinan = Helpers.StatusPerkawinan();
        $scope.KartuKeluarga = $rootScope.SelectedKK;

        $scope.SimpanAnggota = function(model)
        {
            KartuKeluargaService.postanggota(model, $scope.KartuKeluarga ).then(function (response) { })
        }

    })












    ;