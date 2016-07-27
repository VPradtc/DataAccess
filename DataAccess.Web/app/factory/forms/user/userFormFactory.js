angular.module('DataAccessApp').factory('userFormFactory',
['roleDefinitionProvider', UserFormFactory]);

function UserFormFactory(roleDefinitionProvider) {

    var _create = function (emailValidator) {

        var _fields = [
            {
                field: 'email',
                label: 'user.email',
                attrs: {
                    type: 'email',
                    name: 'email',
                    required: '',
                    ngModelOptions: '{ debounce: 1000 }',
                },
                validators: [
                    {
                        async: true,
                        validation: 'unique',
                        validator: emailValidator,
                    }
                ],
                messages: [
                    {
                        validation: 'required',
                        text: 'user.emailIsRequired',
                    },
                    {
                        validation: 'email',
                        text: 'user.invalidEmailFormat',
                    },
                    {
                        validation: 'unique',
                        text: 'user.userWithSuchEmailAlreadyExists',
                    },
                ],
            },
            {
                field: 'firstName',
                label: 'user.firstName',
                attrs: {
                    type: 'text',
                    name: 'firstName',
                    required: '',
                },
                messages: [
                    {
                        validation: 'required',
                        text: 'user.firstNameIsRequired',
                    },
                ],
            },
            {
                field: 'lastName',
                label: 'user.lastName',
                attrs: {
                    type: 'text',
                    name: 'lastName',
                    required: '',
                },
                messages: [
                    {
                        validation: 'required',
                        text: 'user.lastNameIsRequired',
                    },
                ],
            },
            {
                field: 'password',
                label: 'auth.password',
                attrs: {
                    type: 'password',
                    name: 'password',
                    required: '',
                },
                messages: [
                    {
                        validation: 'required',
                        text: 'user.passwordIsRequired',
                    },
                ],
            },
            {
                field: 'phoneNumber',
                label: 'user.phoneNumber',
                attrs: {
                    type: 'text',
                    name: 'phoneNumber',
                },
            },
        ];

        return _fields;
    }

    return {
        create: _create,
    }
}
