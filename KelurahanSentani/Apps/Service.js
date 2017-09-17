angular.module("app.service", [])
    .factory("BaseUrl", function () {
        var service = {};
        service.URL = "http://localhost:52814";
        return service;
    })

    .factory("PagenationService", function ($filter) {

        var service = {};
        service.items = [];
        service.Load = function (items, q, size) {
            this.pageSize = size;
            this.items = items;
            this.q = q;
            this.numberOfPages = Math.ceil(this.items.length / this.pageSize);
            return $filter('filter')(this.items, this.q);
        }
        service.currentPage = 0;
        service.pageSize = 10;
        service.q = '';
        service.numberOfPages = 0;


        service.numberOfPagesData = function () {
            var data = [];
            for (var i = 0; i < this.numberOfPages; i++) {
                data.push(i);
            }
            return data;
        }

        return service;

    })

    .filter('startFrom', function () {
        return function (input, start) {
            start = +start; //parse to int
            return input.slice(start);
        }
    })


    .factory("Helpers", function ()
    {
        var service = {};
        var messages = [];
        Load();

        service.getMessage = function (code,message)
        {
           angular.forEach(messages, function (value, key) {
               if (value.code == code) {
                   message = value.message;
                }
            });

           return message;
        }

      

        function Load()
        {
            messages.push({ code: 1, message: 'Data Berhasil Ditambah' });
            messages.push({ code: 2, message: 'Data Berhasil Diubah' });
            messages.push({ code: 3, message: 'Data Berhasil DiHapus' });
            messages.push({ code: 401, message: 'Anda tidak memiliki hak akses' });
        }


        service.Kepercayaan = function ()
        {
            return ['Islam', 'Protestan', 'Khatolik', 'Hindu', 'Budha', 'Konghuchu'];
        }
        service.JenisKelamin= function () {
            return ['Pria', 'Wanita'];
        }

        service.Pendidikan = function () {
            return ['SD', 'SMP', 'SMA', 'S1', 'S2', 'S3'];
        }


        service.Hubungan = function ()
        {
            return ['KepalaKeluarga', 'Istri', 'Anak', 'Ibu', 'Bapak', 'Famili'];
        }
        service.StatusPerkawinan= function () {
            return ['Kawin', 'Belum'];
        }
        service.Kewarganegaraan = function () {
            return ['WNI', 'WNA'];
        }
        return service;

    })

    .factory("PejabatService", function ($http, $q, BaseUrl,Helpers) {
        var service = {};
       var isInstant = false;
       var collection = [];
        service.source = function ()
        {
            deferred = $q.defer();
            if (!isInstant)
            {
                $http({
                    method: 'GET',
                    url: BaseUrl.URL + "/api/pejabat/get",
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    collection = response.data;
                    deferred.resolve(collection);
                    isInstant = true;
                    }, function (error) {
                        alert(Helpers.getMessage(error.status,error.data.Message));
                   // deferred.reject(error);
                });
               
            } else
            {
                deferred.resolve(collection);
            }

            return deferred.promise;
        }
        service.GetPejabatRW = function (datas)
        {
            if (!isInstant)
            {
               this.source().then(function (response) {
                    angular.forEach(response, function (value, key) {
                        if (value.Level === 'RW') {
                            datas.push(value);
                        }
                    });

                  
                });
             
            }
            else
            {
                angular.forEach(collection, function (value, key) {
                    if (value.Level === 'RW') {
                        datas.push(value);
                    }
                });
               
            }
            
        }

        service.GetPejabatRT = function (datas) {
            if (!isInstant) {
                this.source().then(function (response) {
                    angular.forEach(response, function (value, key) {
                        if (value.Level === 'RT') {
                            datas.push(value);
                        }
                    });


                });

            }
            else {
                angular.forEach(collection, function (value, key) {
                    if (value.Level === 'RT') {
                        datas.push(value);
                    }
                });

            }
        }

        service.Insert = function (model)
        {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.URL + "/api/account/register",
                data: model
            }).then(function (response) {
                collection.push(response.data);
                alert(Helpers.getMessage(1, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

        service.put = function (model,selected) {
            deferred = $q.defer();
            $http({
                method: 'put',
                url: BaseUrl.URL + "/api/pejabat/put?id="+model.Id,
                data: model
            }).then(function (response) {
                selected.Nama = model.Nama;
                selected.Alamat = model.Alamat;
                selected.Level = model.Level;
                selected.Jabatan = model.Jabatan;
                alert(Helpers.getMessage(2, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

       


        service.delete = function(model)
        {
            deferred = $q.defer();
            $http({
                method: 'delete',
                url: BaseUrl.URL + "/api/pejabat/delete?id=" + model.Id,
                data: model
            }).then(function (response) {
                var index = collection.indexOf(model);
                collection.splice(index, 1);     
                alert(Helpers.getMessage(3, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

        return service;
    })

    .factory("StrukturKelurahanService", function ($http, $q, BaseUrl, Helpers) {
        var service = {};
        var isInstant = false;
        var collection = [];

        service.source = function () {
            deferred = $q.defer();
            if (!isInstant) {
                $http({
                    method: 'GET',
                    url: BaseUrl.URL + "/api/StrukturKelurahan/getrw",
                }).then(function (data) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    collection = data;
                    deferred.resolve(collection);
                    isInstant = true;

                }, function (error) {

                    alert(Helpers.getMessage(error.status, error.data.Message));
                    // deferred.reject(error);
                });

            } else {
                deferred.resolve(collection);
            }

            return deferred.promise;
        };

        service.Insert = function(model)
        {
            deferred = $q.defer();
            $http({
                method: 'POST',
                url: BaseUrl.URL + "/api/StrukturKelurahan/postrw",
                data: model
            }).then(function (data) {
                model.Id = data.Id;
                collection.data.push(model);
                deferred.resolve(data);
               
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }


        service.AddRT = function (rw, model) {
            var data = {};
            data.Id = 0;
            data.Nama = model.Nama;
            data.PejabatId = model.Pejabat.Id;
            data.RWId = rw.Id;

            deferred = $q.defer();
            $http({
                method: 'POST',
                url: BaseUrl.URL + "/api/StrukturKelurahan/postrt",
                data: data
            }).then(function (result) {
                data.Id = result.Id;
                rw.DaftarRT.push(data);
                alert(Helpers.getMessage(1, ""));
                deferred.resolve(result);
            }, function (error) {
                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }
        service.putRT = function (model, selected) {
            deferred = $q.defer();
            $http({
                method: 'put',
                url: BaseUrl.URL + "/api/StrukturKelurahan/putRT?id=" + model.Id,
                data: model
            }).then(function (response) {
                selected.Nama = model.Nama;
                selected.Pejabat = model.Pejabat;
                alert(Helpers.getMessage(2, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }


        service.deleteRT = function (model, daftar) {
            deferred = $q.defer();
            $http({
                method: 'delete',
                url: BaseUrl.URL + "/api/StrukturKelurahan/deleteRT?id=" + model.Id,
                data: model
            }).then(function (response) {
                var index = daftar.indexOf(model);
                daftar.splice(index, 1);
                alert(Helpers.getMessage(3, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

        return service;
    })
    .factory("KartuKeluargaService", function ($http, $q, BaseUrl, Helpers) {
        var service = {};
        var isInstant = false;
        var collection = [];
        service.source = function () {
            deferred = $q.defer();
            if (!isInstant) {
                $http({
                    method: 'GET',
                    url: BaseUrl.URL + "/api/kartukeluarga/get",
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    collection = response.data;
                    deferred.resolve(collection);
                    isInstant = true;
                }, function (error) {
                    alert(Helpers.getMessage(error.status, error.data.Message));
                    // deferred.reject(error);
                });

            } else {
                deferred.resolve(collection);
            }

            return deferred.promise;
        };

        service.Insert = function (model) {
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.URL + "/api/kartukeluarga/post",
                data: model
            }).then(function (response) {
                collection.push(response.data);
                alert(Helpers.getMessage(1, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

     
        service.postanggota = function (model, kk) {
            model.KartuKeluarga = kk;
            deferred = $q.defer();
            $http({
                method: 'post',
                url: BaseUrl.URL + "/api/kartukeluarga/postanggota",
                data: model
            }).then(function (response) {
               kk.DaftarKeluarga.push(response.data);
                alert(Helpers.getMessage(2, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        };


        service.putperson = function (model, selected)
        {
            deferred = $q.defer();
            $http({
                method: 'put',
                url: BaseUrl.URL + "/api/kartukeluarga/putanggota",
                data: model
            }).then(function (response) {
                selected.NIK = model.NIK;
                selected.Nama = model.Nama;
                selected.TempatLahir = model.TempatLahir;
                selected.TanggalLahir = model.TanggalLahir;
                selected.Agama = model.Agama;
                selected.JK = model.JK;
                selected.Pekerjaan = model.Pekerjaan;
                selected.Pendidikan = model.Pendidikan;

                selected.Detail.StatusPerkawinan = model.Detail.StatusPerkawinan;
                selected.Detail.HubunganDalamKeluarga = model.Detail.HubunganDalamKeluarga;
                selected.Detail.Kewarganegaraan = model.Detail.Kewarganegaraan;
                selected.Detail.Paspor = model.Detail.Paspor;
                selected.Detail.DokumenLain = model.Detail.DokumenLain;
                selected.Detail.Ayah = model.Detail.Ayah;
                selected.Detail.Ibu = model.Detail.Ibu;


                alert(Helpers.getMessage(2, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }


        service.delete = function (model) {
            deferred = $q.defer();
            $http({
                method: 'delete',
                url: BaseUrl.URL + "/api/pejabat/delete?id=" + model.Id,
                data: model
            }).then(function (response) {
                var index = collection.indexOf(model);
                collection.splice(index, 1);
                alert(Helpers.getMessage(3, ""));
                deferred.resolve(response.data);
            }, function (error) {

                alert(Helpers.getMessage(error.status, error.data.Message));
                // deferred.reject(error);
            });

            return deferred.promise;
        }

        return service;
    })


    ;