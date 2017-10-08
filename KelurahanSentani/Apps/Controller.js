angular.module("app.controller", [])
    .controller("PejabatController", function ($scope, PejabatService,$window,Helpers) {
        $scope.TambahTitle = "Tambah Pejabat";
        $scope.IsNew = true;
        $scope.Pejabats = [];
        $scope.Levels = ["Lurah", "RW", "RT"];
        $scope.JenisJabatans = ["Ketua", "Sekretaris"];
        $scope.MyRole = "";
        PejabatService.source().then(function (data) {
            try {
                $scope.Pejabats = data;
                Helpers.MyRole().then(function (response) {
                    $scope.MyRole = response;
                });
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
        $scope.PejabatRW = [];
        $scope.PejabatRT = [];
        $scope.IsBusy = false;

        $scope.Init = function()
        {
            StrukturKelurahanService.source().then(function (response) {
                $scope.Strukturs = response.data;
                PejabatService.source().then(function (response) {
                    PejabatService.GetPejabatRW($scope.PejabatRW);
                    PejabatService.GetPejabatRT($scope.PejabatRT);
                });
            });

        }

      
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

    .controller("KartuKeluargaController", function ($scope, StrukturKelurahanService, KartuKeluargaService, $window, Helpers, $rootScope, PagenationService) {
        $scope.Strukturs = [];
        $scope.IsBusy = false;
        $scope.Kepercayaan = Helpers.Kepercayaan();
        $scope.JenisKelamin = Helpers.JenisKelamin();
        $scope.Pendidikan = Helpers.Pendidikan();
        $scope.KartuKeluarga = [];
        $scope.Pagenation = PagenationService;
        $scope.Search = '';
        $scope.MyRole = '';

        KartuKeluargaService.source().then(function (response) {
            $scope.KartuKeluarga = $scope.Pagenation.Load(response, $scope.Search, 10); 
            StrukturKelurahanService.source().then(function (response) {
                $scope.Strukturs = response.data;
                Helpers.MyRole().then(function (response) {
                    $scope.MyRole = response;
                });
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
                KartuKeluargaService.Insert(data).then(function (response) {
                    $scope.KartuKeluarga.push(response);
                })
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
        $scope.Pendidikan = Helpers.Pendidikan();
        $scope.StatusPerkawinan = Helpers.StatusPerkawinan();
        $scope.KartuKeluarga = $rootScope.SelectedKK;
        $scope.MyRole = '';
        Helpers.MyRole().then(function (response) {
            $scope.MyRole = response;

        });

        $scope.SimpanAnggota = function(model)
        {
            KartuKeluargaService.postanggota(model, $scope.KartuKeluarga ).then(function (response) { })
        }
        $scope.SelectPerson = function(item)
        {
            $scope.SelectedPerson = item;
            $scope.Person = angular.copy(item);
            $scope.Person.TanggalLahir = new Date(item.TanggalLahir);

        }
        $scope.SimpanEditAnggota = function (person)
        {
            KartuKeluargaService.putperson(person, $scope.SelectedPerson).then(function (response) { });
        }

    })

    .controller("PermohonanController", function ($scope,$http,Helpers, PermohonanService, KartuKeluargaService,SuratService) {
        $scope.Permohonans = [];
        $scope.KartuKeluargas = [];
        $scope.Helpers = Helpers;
        $scope.Actions = ['Setuju', 'Batalkan'];
        $scope.MyRole = "";
        $scope.Init = function ()
        {
            PermohonanService.source().then(function (response) {
                $scope.Permohonans = response;

                Helpers.MyRole().then(function (response) {
                    $scope.MyRole = response;
                    KartuKeluargaService.source().then(function (response) {
                        $scope.KartuKeluargas = response;
                    });
                });
               
            });
        }

        $scope.Selected = function (item)
        {
            angular.forEach($scope.KartuKeluargas, function (value, key) {
                if (value.NoKK == item)
                {
                    $scope.KKSelected = value;
                    $scope.DaftarKeluargas = value.DaftarKeluarga;

                    angular.forEach($scope.DaftarKeluargas, function (value1, key1) {
                        value1.Selected = false;

                    })

                }
            })
        }

        $scope.Insert = function (model) {
            var data = {};
           
            data.RTId = $scope.KKSelected.RTId;
            data.Isi = model.Isi;
            data.JenisSurat = model.JenisSurat;
            data.Id = 0;
            data.PendudukId = model.Penduduk.Id;
            data.EmailPemohon = model.EmailPemohon;
            
            if (data.JenisSurat === 'Pindah')
            {
                data.DataPindah = [];
                angular.forEach($scope.DaftarKeluargas, function (value, key) {
                    if (value.Selected === true)
                    {
                        data.DataPindah.push(value);
                    }
                })
                PermohonanService.Insert(data).then(function () {

                });
            } else
            {
               
                PermohonanService.Insert(data).then(function () {

                });
            }
          
        }

        $scope.ChangePersetujuan = function(item,action)
        {
            if (action === "Setuju")
            {
                PermohonanService.Approved(item, action).then(function (response) {
                    item.StatusPersetujuan.IAproved = true;
                });
            } else {
                item.Status = "Batal";
                PermohonanService.Unapproved(item, action).then(function (response) {
                    item.StatusPersetujuan.IAproved = false;
                });
            }
               
        }

        $scope.DaftarKeluargas = [];

        $scope.BuatSurat = function(item)
        {
            $scope.model = {};
            $scope.model.Surat = {};
            $scope.model.Surat.JenisSurat = item.JenisSurat;
            $scope.model.Surat.TanggalBuat = new Date();
            $scope.model.Surat.BerlakuHingga = new Date();
            $scope.model.Keterangan = item.Isi;
            $scope.model.Penduduk = item.Penduduk;
            $scope.model.NIK = item.Penduduk.NIK;
            $scope.model.Surat.PermohonanId = item.Id;
            if (item.JenisSurat=='Pindah')
            {
                $http({
                    method: 'GET',
                    url: "/api/permohonan/GetAnggotaByPermohonan?id=" + item.Id,
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    $scope.DaftarKeluargas = response.data;
                }, function (error) {
                    // deferred.reject(error);
                });
            }




        }

        $scope.SimpanSurat = function(model)
        {
            switch (model.Surat.JenisSurat) {
                case "Umum":
                    SuratService.SaveToUmum(model).then(function (response) { });
                    break;
                case "Pindah":
                    SuratService.SaveToPindah(model).then(function (response) { });
                    break;
                case "Kematian":
                    SuratService.SaveToKematian(model).then(function (response) { });
                    break;
                default:
            }
        }

        $scope.Edit = function(item)
        {
            $scope.model = angular.copy(item);
        }

        $scope.Detail = function (item) {
            $scope.model =item;
        }

        $scope.Update = function (data) {
            if (data.JenisSurat === 'Pindah') {
                data.DataPindah = [];
                angular.forEach($scope.DaftarKeluargas, function (value, key) {
                    if (value.Selected === true) {
                        data.DataPindah.push(value);
                    }
                });
               
            } 
            PermohonanService.Update(data).then(function () {

            });
        }


    })

    .controller("SuratUmumController", function ($scope, Helpers, SuratService, PejabatService) {
        $scope.Helpers = Helpers;
        $scope.Surats = [];
        $scope.MyRole = '';
        $scope.Init = function () {
            SuratService.sourceumum().then(function (response) {
                $scope.Surats = response;
                Helpers.MyRole().then(function (response) {
                    $scope.MyRole = response;
                    
                });
            });
            
        };

       
    })

    .controller("SuratKematianController", function ($scope, Helpers, SuratService, $rootScope) {
        $scope.Helpers = Helpers;
        $scope.Surats = [];
        $scope.MyRole = '';
        $scope.Init = function () {
            SuratService.sourcekematian().then(function (response) {
                $scope.Surats = response;
                Helpers.MyRole().then(function (response) {
                    $scope.MyRole = response;
                });
            });
        };
        $scope.Print = function (item)
        {
            $rootScope.SuratKematian = item;
        }
    })

    .controller("SuratPindahController", function ($scope, Helpers, SuratService) {
        $scope.Helpers = Helpers;
        $scope.Surats = [];
        $scope.MyRole = '';
        $scope.Init = function () {
            SuratService.sourcepindah().then(function (response) {
                $scope.Surats = response;
                Helpers.MyRole().then(function (response) {
                    $scope.MyRole = response;
                });
            });
        };
    })

    .controller("PendudukController", function ($scope,Helpers, KartuKeluargaService, StrukturKelurahanService, PagenationService) {
        $scope.Pagenation = PagenationService;
        $scope.KartuKeluarga = [];
        $scope.Strukturs = [];
        $scope.MyRole = '';
        $scope.Search = '';
        KartuKeluargaService.source().then(function (response) {
            $scope.KartuKeluarga = $scope.Pagenation.Load(response, $scope.Search, 10);
            StrukturKelurahanService.source().then(function (response) {
                $scope.Strukturs = response.data;
                Helpers.MyRole().then(function (response) {
                    $scope.MyRole = response;
                });
            });
        });
    })

    .controller("ReportUmumController", function ($scope, $http, PejabatService, $routeParams,SuratService) {
        $scope.Data = {};
        $scope.PejabatLurah = {};
        $scope.Surat = {};
        $scope.Init = function ()
        {
            SuratService.GetById($routeParams.Id, $routeParams.Jenis).then(function (response) {
                $scope.Surat = response;
                PejabatService.source().then(function (response) {
                    angular.forEach(response, function (value, key) {
                        if (value.Level == "Kelurahan") {
                            $scope.PejabatLurah = value;
                        }
                    });
                });
            });
        }
    })

    .controller("ReportKematianController", function ($scope, $http, $rootScope, PejabatService,$window) {
        $scope.PejabatLurah = {};
        $scope.Surat = {};
        $scope.Init = function () {
            SuratService.GetById($routeParams.Id, $routeParams.Jenis).then(function (response) {
                $scope.Surat = response;
                PejabatService.source().then(function (response) {
                    angular.forEach(response, function (value, key) {
                        if (value.Level == "Kelurahan") {
                            $scope.PejabatLurah = value;
                        }
                    });
                });
            });
        }
    })

    .controller("ReportPindahController", function ($scope, $http, PejabatService, $routeParams, SuratService,$window) {
        $scope.PejabatLurah = {};
        $scope.Surat = {};
        $scope.Init = function () {
            SuratService.GetById($routeParams.Id, $routeParams.Jenis).then(function (response) {
                $scope.Surat = response;
                PejabatService.source().then(function (response) {
                    angular.forEach(response, function (value, key) {
                        if (value.Level == "Kelurahan") {
                            $scope.PejabatLurah = value;
                        }
                    });
                   
                });
            });
        }
    })

    .controller("ReportKematianController", function ($scope, $http, PejabatService, $routeParams, SuratService) {
        $scope.PejabatLurah = {};
        $scope.Surat = {};
        $scope.Init = function () {
            SuratService.GetById($routeParams.Id, $routeParams.Jenis).then(function (response) {
                $scope.Surat = response;
                PejabatService.source().then(function (response) {
                    angular.forEach(response, function (value, key) {
                        if (value.Level == "Kelurahan") {
                            $scope.PejabatLurah = value;
                        }
                    });
                });
            });
        }
    });


    ;