$(function () {   
    var l = abp.localization.getResource("Zero");
	var storeUserService = window.zero.controllers.storeUsers.storeUser;
	
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: "/Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	    $('#AppUserFilterLookupOpenButton').on('click', '', function () {
        lastNpDisplayNameId = 'AppUser_Filter_Email';
        lastNpIdId = 'AppUserIdFilter';
        _lookupModal.open({
            currentId: $('#AppUserIdFilter').val(),
            currentDisplayName: $('#AppUser_Filter_Email').val(),
            serviceMethod: function () {
                return window.zero.controllers.storeUsers.storeUser.getAppUserLookup;
            }
        });
    });	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "StoreUsers/CreateModal",
        scriptUrl: "/Pages/StoreUsers/createModal.js",
        modalClass: "storeUserCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "StoreUsers/EditModal",
        scriptUrl: "/Pages/StoreUsers/editModal.js",
        modalClass: "storeUserEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),	
            desc: $("#DescFilter").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			storeId: $("#StoreIdFilter").val(),			appUserId: $("#AppUserIdFilter").val()
        };
    };
	
    var dataTable = $("#StoreUsersTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(storeUserService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Zero.StoreUsers.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.storeUser.id });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Zero.StoreUsers.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    storeUserService.delete(data.record.storeUser.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "storeUser.desc" }
,            {
                data: "storeUser.isActive",
                render: function (isActive) {
                    return isActive ? l("Yes") : l("No");
                }
            }

            ,
            {
                data: "store.name",
                defaultContent : ""
            },
            {
                data: "appUser.email",
                defaultContent : ""
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewStoreUserButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });
	
    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
});