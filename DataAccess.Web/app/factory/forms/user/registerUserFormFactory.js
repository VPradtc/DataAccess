angular.module('DataAccessApp').factory('registerUserFormFactory',
['roleDefinitionProvider', 'userFormFactory', RegisterUserFormFactory]);

function RegisterUserFormFactory(roleDefinitionProvider, userFormFactory) {

    var _createLocalFieldEntries = function (passwordMatchValidator) {

        var _fieldEntries = [
            {
                insertAfter: 'password',
                field: {
                    field: 'passwordRepeat',
                    label: 'auth.repeatPassword',
                    attrs: {
                        type: 'password',
                        name: 'passwordRepeat',
                        required: '',
                    },
                    validators: [
                        {
                            validation: 'match',
                            validator: passwordMatchValidator,
                        }
                    ],
                    messages: [
                        {
                            validation: 'required',
                            text: 'user.passwordIsRequired'
                        },
                        {
                            validation: 'match',
                            text: 'auth.passwordsDoNotMatch'
                        },
                    ],
                }
            },
        ];

        return _fieldEntries;
    }

    var _create = function (emailValidator, passwordMatchValidator) {

        var fields = userFormFactory.create(emailValidator);
        var localEntries = _createLocalFieldEntries(passwordMatchValidator);

        localEntries.forEach(function (entry) {

            var previousFieldIndex = fields.findIndex(function (item) {
                return item.field === entry.insertAfter;
            });

            var targetIndex = previousFieldIndex + 1;

            fields.splice(targetIndex, 0, entry.field);
        });

        return fields;
    }

    return {
        create: _create,
    }
}
