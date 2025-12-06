// =============================================
// SmartPromptLibrary - Gestión de Versiones
// =============================================

$(document).ready(function () {
    var markdownConverter = new showdown.Converter();

    // =============================================
    // Ver Historial de Versiones
    // =============================================
    $(document).on('click', '#btnVerVersiones', function () {
        var idPrompt = $('#ejecutarPromptId').val();
        var titulo = $('#ejecutarPromptTitle').text();
        cargarHistorialVersiones(idPrompt, titulo);
    });

    function cargarHistorialVersiones(idPrompt, titulo) {
        $('#versionesPromptId').val(idPrompt);
        $('#versionesPromptTitulo').text(' - ' + titulo);

        $.get('/api/smart-prompt-library/prompts/' + idPrompt + '/versiones', function (versiones) {
            var tbody = $('#versionesTableBody');
            tbody.empty();

            if (!versiones || versiones.length === 0) {
                tbody.append('<tr><td colspan="6" class="text-center text-muted">No hay versiones disponibles</td></tr>');
                return;
            }

            versiones.forEach(function (v) {
                var badge = v.esVersionActual ? '<span class="label label-success">Actual</span>' : '';
                var fecha = new Date(v.fechaCreacion).toLocaleString('es-ES');

                var row = '<tr>' +
                    '<td><strong>v' + v.numeroVersion + '</strong> ' + badge + '</td>' +
                    '<td>' + (v.titulo || '-') + '</td>' +
                    '<td>' + (v.mensajeCambio || '-') + '</td>' +
                    '<td>' + (v.creadoPorUsuario || 'Sistema') + '</td>' +
                    '<td>' + fecha + '</td>' +
                    '<td>' +
                    '<button class="btn btn-xs btn-info btnVerDetalleVersion" data-id="' + v.idVersionPrompt + '" data-version="' + v.numeroVersion + '" data-id-prompt="' + v.idPrompt + '">' +
                    '<i class="fa fa-eye"></i>' +
                    '</button> ';

                if (!v.esVersionActual) {
                    row += '<button class="btn btn-xs btn-warning btnRestaurarVersionDesdeLista" data-id-prompt="' + v.idPrompt + '" data-version="' + v.numeroVersion + '">' +
                        '<i class="fa fa-undo"></i>' +
                        '</button>';
                }

                row += '</td></tr>';
                tbody.append(row);
            });

            $('#modalVersiones').modal('show');
        }).fail(function () {
            alert('Error al cargar el historial de versiones');
        });
    }

    // =============================================
    // Ver Detalle de una Versión
    // =============================================
    $(document).on('click', '.btnVerDetalleVersion', function () {
        var idVersion = $(this).data('id');
        var numeroVersion = $(this).data('version');
        var idPrompt = $(this).data('id-prompt');

        $.get('/api/smart-prompt-library/versiones/' + idVersion, function (version) {
            $('#detalleVersionId').val(idVersion);
            $('#detalleVersionIdPrompt').val(idPrompt);
            $('#detalleVersionNumero').text(numeroVersion);
            $('#detalleVersionTitulo').text(version.titulo || '-');
            $('#detalleVersionDescripcion').text(version.descripcion || '-');
            $('#detalleVersionMensajeCambio').text(version.mensajeCambio || '-');
            $('#detalleVersionUsuario').text(version.creadoPorUsuario || 'Sistema');
            $('#detalleVersionFecha').text(new Date(version.fechaCreacion).toLocaleString('es-ES'));

            // Renderizar contenido markdown
            var html = markdownConverter.makeHtml(version.contenidoMarkdown);
            $('#detalleVersionContenido').html(html);

            // Mostrar mensaje de sistema
            $('#detalleVersionMensajeSistema').text(version.mensajeSistema || '(Sin mensaje de sistema)');

            // Ocultar botón de restaurar si es la versión actual
            if (version.esVersionActual) {
                $('#btnRestaurarVersion').hide();
            } else {
                $('#btnRestaurarVersion').show();
            }

            $('#modalDetalleVersion').modal('show');
        }).fail(function () {
            alert('Error al cargar los detalles de la versión');
        });
    });

    // =============================================
    // Restaurar Versión (desde detalle)
    // =============================================
    $(document).on('click', '#btnRestaurarVersion', function () {
        var idPrompt = $('#detalleVersionIdPrompt').val();
        var numeroVersion = $('#detalleVersionNumero').text();

        if (!confirm('¿Está seguro que desea restaurar la versión ' + numeroVersion + '?\\n\\nEsto creará una nueva versión con el contenido de la versión ' + numeroVersion + '.')) {
            return;
        }

        var usuario = prompt('Ingrese su nombre de usuario:');
        if (!usuario) {
            usuario = 'Sistema';
        }

        restaurarVersion(idPrompt, numeroVersion, usuario);
    });

    // =============================================
    // Restaurar Versión (desde lista)
    // =============================================
    $(document).on('click', '.btnRestaurarVersionDesdeLista', function () {
        var idPrompt = $(this).data('id-prompt');
        var numeroVersion = $(this).data('version');

        if (!confirm('¿Está seguro que desea restaurar la versión ' + numeroVersion + '?\\n\\nEsto creará una nueva versión con el contenido de la versión ' + numeroVersion + '.')) {
            return;
        }

        var usuario = prompt('Ingrese su nombre de usuario:');
        if (!usuario) {
            usuario = 'Sistema';
        }

        restaurarVersion(idPrompt, numeroVersion, usuario);
    });

    function restaurarVersion(idPrompt, numeroVersion, usuario) {
        $.ajax({
            url: '/api/smart-prompt-library/prompts/' + idPrompt + '/restaurar-version',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                numeroVersion: parseInt(numeroVersion),
                usuario: usuario
            }),
            success: function (response) {
                if (response.success) {
                    alert('Versión restaurada exitosamente como versión ' + response.numeroVersion);
                    $('#modalDetalleVersion').modal('hide');
                    $('#modalVersiones').modal('hide');

                    // Recargar lista de prompts si está disponible
                    if (typeof cargarPrompts === 'function') {
                        cargarPrompts();
                    }
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: function () {
                alert('Error al restaurar la versión');
            }
        });
    }

    // =============================================
    // Comparar Versiones
    // =============================================
    window.compararVersiones = function (idPrompt, version1, version2) {
        $.get('/api/smart-prompt-library/prompts/' + idPrompt + '/comparar-versiones', {
            version1: version1,
            version2: version2
        }, function (response) {
            if (response.success) {
                // Aquí puedes implementar una vista de comparación más elaborada
                console.log('Versión 1:', response.version1);
                console.log('Versión 2:', response.version2);
                alert('Comparación de versiones:\n\nVersión ' + version1 + ': ' + response.version1.titulo + '\nVersión ' + version2 + ': ' + response.version2.titulo);
            } else {
                alert('Error: ' + response.message);
            }
        }).fail(function () {
            alert('Error al comparar versiones');
        });
    };

    // =============================================
    // Limpiar Versiones Antiguas
    // =============================================
    window.limpiarVersionesAntiguas = function (idPrompt, mantenerUltimas) {
        mantenerUltimas = mantenerUltimas || 10;

        if (!confirm('¿Está seguro que desea eliminar versiones antiguas?\\n\\nSe mantendrán solo las últimas ' + mantenerUltimas + ' versiones.')) {
            return;
        }

        $.ajax({
            url: '/api/smart-prompt-library/prompts/' + idPrompt + '/limpiar-versiones?mantenerUltimas=' + mantenerUltimas,
            type: 'DELETE',
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    cargarHistorialVersiones(idPrompt, '');
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: function () {
                alert('Error al limpiar versiones antiguas');
            }
        });
    };
});
