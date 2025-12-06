// SmartPromptLibrary - JavaScript
// Gestión de biblioteca de prompts de IA

const SmartPromptLibrary = {
    // Estado de la aplicación
    state: {
        prompts: [],
        categorias: [],
        etiquetas: [],
        filtros: {
            search: '',
            categoria: null,
            etiquetas: [],
            soloFavoritos: false
        }
    },

    // Inicialización
    init: function() {
        this.initMarkdownConverter();
        this.loadInitialData();
        this.bindEvents();
        this.initDataTables();
    },

    // Inicializar convertidor de Markdown
    initMarkdownConverter: function() {
        this.markdownConverter = new showdown.Converter({
            tables: true,
            strikethrough: true,
            tasklists: true,
            ghCodeBlocks: true
        });
    },

    // Cargar datos iniciales
    loadInitialData: function() {
        this.loadCategorias();
        this.loadEtiquetas();
        this.loadPrompts();
        this.loadHistorial();
    },

    // Bind de eventos
    bindEvents: function() {
        // Biblioteca
        $('#btnNuevoPrompt').click(() => this.showModalNuevoPrompt());
        $('#btnGenerarConIA').click(() => this.showModalGenerarIA());
        $('#btnGuardarPrompt').click(() => this.guardarPrompt());
        $('#searchPrompts').on('input', (e) => this.handleSearch(e.target.value));
        $('#filterCategoria').change((e) => this.handleFilterCategoria(e.target.value));
        $('#filterEtiquetas').change(() => this.handleFilterEtiquetas());
        $('#btnMostrarFavoritos').click(() => this.toggleFavoritos());

        // Generar con IA
        $('#btnGenerarPrompt').click(() => this.generarPromptConIA());
        $('#btnUsarGenerado').click(() => this.usarPromptGenerado());
        $('#btnSugerirEtiquetas').click(() => this.sugerirEtiquetas());

        // Ejecutar Prompt
        $('#btnEjecutarPrompt').click(() => this.ejecutarPrompt());
        $('#btnEditarPrompt').click(() => this.editarDesdeEjecutar());
        $('#btnCopiarResultado').click(() => this.copiarResultado());

        // Categorías
        $('#btnNuevaCategoria').click(() => this.showModalCategoria());

        // Etiquetas
        $('#btnNuevaEtiqueta').click(() => this.showModalEtiqueta());

        // Select2
        $('.select2').select2();
    },

    // Inicializar DataTables
    initDataTables: function() {
        $('#categoriasTable').DataTable({
            language: { url: '//cdn.datatables.net/plug-ins/1.10.24/i18n/Spanish.json' }
        });

        $('#etiquetasTable').DataTable({
            language: { url: '//cdn.datatables.net/plug-ins/1.10.24/i18n/Spanish.json' }
        });

        $('#historialTable').DataTable({
            language: { url: '//cdn.datatables.net/plug-ins/1.10.24/i18n/Spanish.json' },
            order: [[2, 'desc']] // Ordenar por fecha descendente
        });
    },

    // ========== CATEGORÍAS ==========
    loadCategorias: function() {
        $.get('/api/smart-prompt-library/categorias', (data) => {
            this.state.categorias = data;
            this.renderCategoriasTable();
            this.updateCategoriasDropdown();
        });
    },

    renderCategoriasTable: function() {
        const table = $('#categoriasTable').DataTable();
        table.clear();

        this.state.categorias.forEach(cat => {
            table.row.add([
                cat.idCategoriaPrompt,
                '<i class="' + cat.icono + '" style="color:' + cat.color + '"></i> ' + cat.nombre,
                cat.descripcion || '-',
                '<span style="color:' + cat.color + '; font-size: 20px;"><i class="' + cat.icono + '"></i></span>',
                cat.totalPrompts || 0,
                cat.activo ? '<span class="label label-success">Activo</span>' : '<span class="label label-danger">Inactivo</span>',
                '<button class="btn btn-xs btn-warning" onclick="SmartPromptLibrary.editarCategoria(' + cat.idCategoriaPrompt + ')"><i class="fa fa-edit"></i></button>'
            ]);
        });

        table.draw();
    },

    updateCategoriasDropdown: function() {
        const selectores = ['#promptCategoria', '#filterCategoria'];
        selectores.forEach(selector => {
            const $select = $(selector);
            const currentValue = $select.val();

            $select.empty();
            if (selector === '#filterCategoria') {
                $select.append('<option value="">Todas las categorías</option>');
            }

            this.state.categorias.forEach(cat => {
                if (cat.activo) {
                    $select.append('<option value="' + cat.idCategoriaPrompt + '">' + cat.nombre + '</option>');
                }
            });

            if (currentValue) {
                $select.val(currentValue);
            }
        });
    },

    // ========== ETIQUETAS ==========
    loadEtiquetas: function() {
        $.get('/api/smart-prompt-library/etiquetas', (data) => {
            this.state.etiquetas = data;
            this.renderEtiquetasTable();
            this.updateEtiquetasDropdown();
        });
    },

    renderEtiquetasTable: function() {
        const table = $('#etiquetasTable').DataTable();
        table.clear();

        this.state.etiquetas.forEach(tag => {
            table.row.add([
                tag.idEtiquetaPrompt,
                '<span class="badge-tag" style="background-color:' + tag.color + '; color: white;">' + tag.nombre + '</span>',
                '<span style="display:inline-block; width:20px; height:20px; background-color:' + tag.color + '; border-radius:3px;"></span>',
                tag.totalPrompts || 0,
                tag.activo ? '<span class="label label-success">Activo</span>' : '<span class="label label-danger">Inactivo</span>',
                '<button class="btn btn-xs btn-warning" onclick="SmartPromptLibrary.editarEtiqueta(' + tag.idEtiquetaPrompt + ')"><i class="fa fa-edit"></i></button>'
            ]);
        });

        table.draw();
    },

    updateEtiquetasDropdown: function() {
        const selectores = ['#promptEtiquetas', '#filterEtiquetas'];
        selectores.forEach(selector => {
            const $select = $(selector);
            const currentValues = $select.val() || [];

            $select.empty();

            this.state.etiquetas.forEach(tag => {
                if (tag.activo) {
                    $select.append('<option value="' + tag.idEtiquetaPrompt + '">' + tag.nombre + '</option>');
                }
            });

            if (currentValues.length > 0) {
                $select.val(currentValues);
            }

            $select.trigger('change');
        });
    },

    // ========== PROMPTS ==========
    loadPrompts: function() {
        const url = this.state.filtros.soloFavoritos
            ? '/api/smart-prompt-library/prompts/favoritos'
            : '/api/smart-prompt-library/prompts';

        $.get(url, (data) => {
            this.state.prompts = data;
            this.filterAndRenderPrompts();
        });
    },

    filterAndRenderPrompts: function() {
        let filtered = [...this.state.prompts];

        // Filtro de búsqueda
        if (this.state.filtros.search) {
            const search = this.state.filtros.search.toLowerCase();
            filtered = filtered.filter(p =>
                p.titulo.toLowerCase().includes(search) ||
                (p.descripcion && p.descripcion.toLowerCase().includes(search))
            );
        }

        // Filtro de categoría
        if (this.state.filtros.categoria) {
            filtered = filtered.filter(p => p.idCategoriaPrompt == this.state.filtros.categoria);
        }

        // Filtro de etiquetas
        if (this.state.filtros.etiquetas.length > 0) {
            // Implementar filtro por etiquetas (requiere cargar etiquetas por prompt)
        }

        this.renderPromptsList(filtered);
    },

    renderPromptsList: function(prompts) {
        const $container = $('#promptsList');
        $container.empty();

        if (prompts.length === 0) {
            $container.html('<div class="col-md-12"><div class="alert alert-info">No se encontraron prompts</div></div>');
            return;
        }

        prompts.forEach(prompt => {
            const card = this.createPromptCard(prompt);
            $container.append(card);
        });
    },

    createPromptCard: function(prompt) {
        const favoritoClass = prompt.favorito ? 'favorito' : '';
        const favoritoIcon = prompt.favorito ? 'fa-star' : 'fa-star-o';

        return '<div class="col-md-4">' +
            '<div class="box box-widget prompt-card ' + favoritoClass + '" data-id="' + prompt.idPrompt + '" onclick="SmartPromptLibrary.verPrompt(' + prompt.idPrompt + ')">' +
            '<div class="box-header with-border">' +
            '<div class="user-block">' +
            '<span class="category-icon" style="background-color:' + (prompt.categoriaColor || '#999') + '">' +
            '<i class="' + (prompt.categoriaIcono || 'fa fa-file') + '"></i>' +
            '</span>' +
            '<span class="username" style="margin-left: 10px;">' + prompt.titulo + '</span>' +
            '<span class="description">' + (prompt.categoriaNombre || '') + '</span>' +
            '</div>' +
            '<div class="box-tools">' +
            '<button type="button" class="btn btn-box-tool" onclick="event.stopPropagation(); SmartPromptLibrary.toggleFavorito(' + prompt.idPrompt + ')">' +
            '<i class="fa ' + favoritoIcon + ' text-warning"></i>' +
            '</button>' +
            '</div>' +
            '</div>' +
            '<div class="box-body">' +
            '<p>' + (prompt.descripcion || 'Sin descripción') + '</p>' +
            '<div class="text-muted">' +
            '<small><i class="fa fa-play-circle"></i> ' + (prompt.cantidadEjecuciones || 0) + ' ejecuciones</small>' +
            '</div>' +
            '</div>' +
            '<div class="box-footer">' +
            '<button class="btn btn-sm btn-primary" onclick="event.stopPropagation(); SmartPromptLibrary.verPrompt(' + prompt.idPrompt + ')">' +
            '<i class="fa fa-play"></i> Ejecutar' +
            '</button>' +
            '<button class="btn btn-sm btn-default" onclick="event.stopPropagation(); SmartPromptLibrary.editarPrompt(' + prompt.idPrompt + ')">' +
            '<i class="fa fa-edit"></i>' +
            '</button>' +
            '<button class="btn btn-sm btn-danger pull-right" onclick="event.stopPropagation(); SmartPromptLibrary.eliminarPrompt(' + prompt.idPrompt + ')">' +
            '<i class="fa fa-trash"></i>' +
            '</button>' +
            '</div>' +
            '</div>' +
            '</div>';
    },

    // ========== GENERAR CON IA ==========
    showModalGenerarIA: function() {
        $('#ideaGeneral').val('');
        $('#promptGenerado').hide();
        $('#btnUsarGenerado').hide();
        $('#btnGenerarPrompt').show();
        $('#modalGenerarIA').modal('show');
    },

    generarPromptConIA: function() {
        const idea = $('#ideaGeneral').val().trim();
        if (!idea) {
            alert('Por favor, describe tu idea');
            return;
        }

        $('#btnGenerarPrompt').prop('disabled', true).html('<i class="fa fa-spinner fa-spin"></i> Generando...');

        $.ajax({
            url: '/api/smart-prompt-library/prompts/generar',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ ideaGeneral: idea }),
            success: (response) => {
                if (response.success) {
                    const html = this.markdownConverter.makeHtml(response.contenidoGenerado);
                    $('#promptGeneradoPreview').html(html);
                    $('#promptGenerado').show();
                    $('#btnGenerarPrompt').hide();
                    $('#btnUsarGenerado').show();
                    this.state.promptGeneradoTemp = response.contenidoGenerado;
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: (xhr) => {
                alert('Error al generar el prompt');
            },
            complete: () => {
                $('#btnGenerarPrompt').prop('disabled', false).html('<i class="fa fa-magic"></i> Generar');
            }
        });
    },

    usarPromptGenerado: function() {
        $('#modalGenerarIA').modal('hide');
        this.showModalNuevoPrompt();
        $('#promptContenido').val(this.state.promptGeneradoTemp);
    },

    // ========== CRUD PROMPTS ==========
    showModalNuevoPrompt: function() {
        $('#promptId').val('');
        $('#promptTitulo').val('');
        $('#promptDescripcion').val('');
        $('#promptCategoria').val('');
        $('#promptContenido').val('');
        $('#promptSistema').val('');
        $('#promptEtiquetas').val(null).trigger('change');
        $('#promptFavorito').prop('checked', false);
        $('#modalPromptTitle').text('Nuevo Prompt');
        $('#modalPrompt').modal('show');
    },

    guardarPrompt: function() {
        const promptData = {
            idCategoriaPrompt: parseInt($('#promptCategoria').val()),
            titulo: $('#promptTitulo').val().trim(),
            descripcion: $('#promptDescripcion').val().trim(),
            contenidoMarkdown: $('#promptContenido').val().trim(),
            mensajeSistema: $('#promptSistema').val().trim(),
            favorito: $('#promptFavorito').is(':checked'),
            activo: true,
            idsEtiquetas: $('#promptEtiquetas').val() ? $('#promptEtiquetas').val().map(id => parseInt(id)) : []
        };

        // Validaciones
        if (!promptData.titulo) {
            alert('El título es requerido');
            return;
        }
        if (!promptData.idCategoriaPrompt) {
            alert('La categoría es requerida');
            return;
        }
        if (!promptData.contenidoMarkdown) {
            alert('El contenido es requerido');
            return;
        }

        const promptId = $('#promptId').val();
        const isEdit = promptId !== '';
        const url = isEdit
            ? '/api/smart-prompt-library/prompts/' + promptId
            : '/api/smart-prompt-library/prompts';
        const method = isEdit ? 'PUT' : 'POST';

        $.ajax({
            url: url,
            method: method,
            contentType: 'application/json',
            data: JSON.stringify(promptData),
            success: (response) => {
                if (response.success) {
                    $('#modalPrompt').modal('hide');
                    this.loadPrompts();
                    alert('Prompt guardado exitosamente');
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: () => {
                alert('Error al guardar el prompt');
            }
        });
    },

    sugerirEtiquetas: function() {
        const titulo = $('#promptTitulo').val().trim();
        const descripcion = $('#promptDescripcion').val().trim();
        const contenido = $('#promptContenido').val().trim();

        if (!titulo || !contenido) {
            alert('Completa al menos el título y el contenido para sugerir etiquetas');
            return;
        }

        $('#btnSugerirEtiquetas').prop('disabled', true).html('<i class="fa fa-spinner fa-spin"></i>');

        $.ajax({
            url: '/api/smart-prompt-library/prompts/sugerir-etiquetas',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ titulo, descripcion, contenidoMarkdown: contenido }),
            success: (response) => {
                if (response.success && response.etiquetasSugeridas) {
                    // Seleccionar etiquetas sugeridas que existan
                    const idsEtiquetas = [];
                    response.etiquetasSugeridas.forEach(nombreTag => {
                        const etiqueta = this.state.etiquetas.find(e =>
                            e.nombre.toLowerCase() === nombreTag.toLowerCase()
                        );
                        if (etiqueta) {
                            idsEtiquetas.push(etiqueta.idEtiquetaPrompt.toString());
                        }
                    });

                    if (idsEtiquetas.length > 0) {
                        $('#promptEtiquetas').val(idsEtiquetas).trigger('change');
                    } else {
                        alert('No se encontraron coincidencias con las etiquetas existentes');
                    }
                } else {
                    alert('Error al sugerir etiquetas: ' + (response.message || 'Error desconocido'));
                }
            },
            error: () => {
                alert('Error al sugerir etiquetas');
            },
            complete: () => {
                $('#btnSugerirEtiquetas').prop('disabled', false).html('<i class="fa fa-magic"></i>');
            }
        });
    },

    editarPrompt: function(id) {
        $.get('/api/smart-prompt-library/prompts/' + id, (prompt) => {
            $('#promptId').val(prompt.idPrompt);
            $('#promptTitulo').val(prompt.titulo);
            $('#promptDescripcion').val(prompt.descripcion || '');
            $('#promptCategoria').val(prompt.idCategoriaPrompt);
            $('#promptContenido').val(prompt.contenidoMarkdown);
            $('#promptSistema').val(prompt.mensajeSistema || '');
            $('#promptFavorito').prop('checked', prompt.favorito);

            // Cargar etiquetas
            if (prompt.etiquetas && prompt.etiquetas.length > 0) {
                const idsEtiquetas = prompt.etiquetas.map(e => e.idEtiquetaPrompt.toString());
                $('#promptEtiquetas').val(idsEtiquetas).trigger('change');
            } else {
                $('#promptEtiquetas').val(null).trigger('change');
            }

            $('#modalPromptTitle').text('Editar Prompt');
            $('#modalPrompt').modal('show');
        });
    },

    eliminarPrompt: function(id) {
        if (!confirm('¿Estás seguro de eliminar este prompt?')) {
            return;
        }

        $.ajax({
            url: '/api/smart-prompt-library/prompts/' + id,
            method: 'DELETE',
            success: (response) => {
                if (response.success) {
                    this.loadPrompts();
                    alert('Prompt eliminado exitosamente');
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: () => {
                alert('Error al eliminar el prompt');
            }
        });
    },

    toggleFavorito: function(id) {
        $.ajax({
            url: '/api/smart-prompt-library/prompts/' + id + '/favorito',
            method: 'PUT',
            success: (response) => {
                if (response.success) {
                    this.loadPrompts();
                }
            }
        });
    },

    // ========== VER Y EJECUTAR PROMPT ==========
    verPrompt: function(id) {
        $.get('/api/smart-prompt-library/prompts/' + id, (prompt) => {
            $('#ejecutarPromptId').val(prompt.idPrompt);
            $('#ejecutarPromptTitle').text(prompt.titulo);
            $('#ejecutarPromptDescripcion').text(prompt.descripcion || 'Sin descripción');

            // Renderizar etiquetas
            let etiquetasHtml = '';
            if (prompt.etiquetas && prompt.etiquetas.length > 0) {
                prompt.etiquetas.forEach(tag => {
                    etiquetasHtml += '<span class="badge-tag" style="background-color:' + tag.color + '; color: white;">' + tag.nombre + '</span> ';
                });
                $('#ejecutarPromptEtiquetas').html(etiquetasHtml);
            } else {
                $('#ejecutarPromptEtiquetas').html('');
            }

            // Renderizar contenido markdown
            const html = this.markdownConverter.makeHtml(prompt.contenidoMarkdown);
            $('#ejecutarPromptContenido').html(html);

            // Detectar variables y crear formulario
            $.get('/api/smart-prompt-library/prompts/' + id + '/variables', (response) => {
                if (response.success && response.variables.length > 0) {
                    let formHtml = '<h4>Parámetros del Prompt</h4>';
                    response.variables.forEach(variable => {
                        formHtml += '<div class="variable-input">' +
                            '<label>' + variable + ':</label>' +
                            '<textarea class="form-control" data-variable="' + variable + '" rows="2"></textarea>' +
                            '</div>';
                    });
                    $('#parametrosContainer').html(formHtml);
                } else {
                    $('#parametrosContainer').html('<p class="text-muted">Este prompt no requiere parámetros</p>');
                }
            });

            $('#resultadoEjecucion').hide();
            $('#modalEjecutarPrompt').modal('show');
        });
    },

    ejecutarPrompt: function() {
        const id = $('#ejecutarPromptId').val();

        // Recopilar parámetros
        const parametros = {};
        $('#parametrosContainer textarea[data-variable]').each(function() {
            const variable = $(this).data('variable');
            const valor = $(this).val();
            if (valor) {
                parametros[variable] = valor;
            }
        });

        $('#btnEjecutarPrompt').prop('disabled', true).html('<i class="fa fa-spinner fa-spin"></i> Ejecutando...');

        $.ajax({
            url: '/api/smart-prompt-library/prompts/' + id + '/ejecutar',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ parametros: parametros }),
            success: (response) => {
                if (response.success) {
                    // Mostrar resultado
                    const htmlRespuesta = this.markdownConverter.makeHtml(response.respuesta);
                    $('#resultadoContenido').html(htmlRespuesta);
                    $('#resultadoTokens').text(response.tokensUsados || 'N/A');
                    $('#resultadoCosto').text('$' + (response.costoEstimado || 0).toFixed(6));
                    $('#resultadoTiempo').text(response.tiempoMs + 'ms');
                    $('#resultadoEjecucion').show();

                    // Guardar para copiar
                    this.state.ultimoResultado = response.respuesta;

                    // Actualizar historial
                    this.loadHistorial();

                    // Scroll al resultado
                    $('#resultadoEjecucion')[0].scrollIntoView({ behavior: 'smooth' });
                } else {
                    alert('Error al ejecutar el prompt: ' + response.message);
                }
            },
            error: () => {
                alert('Error al ejecutar el prompt');
            },
            complete: () => {
                $('#btnEjecutarPrompt').prop('disabled', false).html('<i class="fa fa-play"></i> Ejecutar Prompt');
            }
        });
    },

    editarDesdeEjecutar: function() {
        const id = $('#ejecutarPromptId').val();
        $('#modalEjecutarPrompt').modal('hide');
        this.editarPrompt(id);
    },

    copiarResultado: function() {
        if (this.state.ultimoResultado) {
            navigator.clipboard.writeText(this.state.ultimoResultado).then(() => {
                alert('Resultado copiado al portapapeles');
            });
        }
    },

    // ========== HISTORIAL ==========
    loadHistorial: function() {
        $.get('/api/smart-prompt-library/ejecuciones?limit=50', (data) => {
            this.renderHistorialTable(data);
        });
    },

    renderHistorialTable: function(ejecuciones) {
        const table = $('#historialTable').DataTable();
        table.clear();

        ejecuciones.forEach(ej => {
            const fecha = new Date(ej.fechaEjecucion).toLocaleString('es-ES');
            const estado = ej.exitoso
                ? '<span class="label label-success"><i class="fa fa-check"></i> Exitoso</span>'
                : '<span class="label label-danger"><i class="fa fa-times"></i> Error</span>';

            table.row.add([
                ej.idEjecucionPrompt,
                ej.promptTitulo,
                fecha,
                estado,
                ej.tokensUsados || '-',
                ej.costoEstimado ? '$' + ej.costoEstimado.toFixed(6) : '-',
                ej.tiempoRespuestaMs || '-',
                '<button class="btn btn-xs btn-info" onclick="SmartPromptLibrary.verDetalleEjecucion(' + ej.idEjecucionPrompt + ')"><i class="fa fa-eye"></i></button>'
            ]);
        });

        table.draw();
    },

    verDetalleEjecucion: function(id) {
        $.get('/api/smart-prompt-library/ejecuciones/' + id, (ejecucion) => {
            let html = '<div class="row">' +
                '<div class="col-md-6">' +
                '<h4>Información General</h4>' +
                '<p><strong>Prompt:</strong> ' + ejecucion.promptTitulo + '</p>' +
                '<p><strong>Fecha:</strong> ' + new Date(ejecucion.fechaEjecucion).toLocaleString('es-ES') + '</p>' +
                '<p><strong>Estado:</strong> ' + (ejecucion.exitoso ? 'Exitoso' : 'Error') + '</p>' +
                '<p><strong>Tokens:</strong> ' + (ejecucion.tokensUsados || 'N/A') + '</p>' +
                '<p><strong>Costo:</strong> $' + (ejecucion.costoEstimado || 0).toFixed(6) + '</p>' +
                '</div>' +
                '<div class="col-md-6">' +
                '<h4>Configuración</h4>' +
                '<p><strong>Proveedor:</strong> ' + (ejecucion.proveedorNombre || 'N/A') + '</p>' +
                '<p><strong>Modelo:</strong> ' + (ejecucion.modeloNombre || 'N/A') + '</p>' +
                '<p><strong>Temperatura:</strong> ' + (ejecucion.temperatura || 'N/A') + '</p>' +
                '<p><strong>Max Tokens:</strong> ' + (ejecucion.maxTokens || 'N/A') + '</p>' +
                '</div>' +
                '</div>';

            // Parámetros
            if (ejecucion.parametros && ejecucion.parametros.length > 0) {
                html += '<hr><h4>Parámetros Utilizados</h4><ul>';
                ejecucion.parametros.forEach(p => {
                    html += '<li><strong>' + p.nombreParametro + ':</strong> ' + p.valorParametro + '</li>';
                });
                html += '</ul>';
            }

            // Prompt Final
            html += '<hr><h4>Prompt Final Enviado</h4>' +
                '<pre style="background:#f4f4f4; padding:15px; border-radius:4px; max-height:300px; overflow-y:auto;">' +
                ejecucion.promptFinal + '</pre>';

            // Respuesta
            if (ejecucion.exitoso) {
                const respuestaHtml = this.markdownConverter.makeHtml(ejecucion.respuestaIA);
                html += '<hr><h4>Respuesta de la IA</h4>' +
                    '<div class="markdown-preview">' + respuestaHtml + '</div>';
            } else {
                html += '<hr><h4>Error</h4>' +
                    '<div class="alert alert-danger">' + (ejecucion.mensajeError || 'Error desconocido') + '</div>';
            }

            $('#detalleEjecucionContenido').html(html);
            $('#modalDetalleEjecucion').modal('show');
        });
    },

    // ========== FILTROS ==========
    handleSearch: function(searchTerm) {
        this.state.filtros.search = searchTerm;
        this.filterAndRenderPrompts();
    },

    handleFilterCategoria: function(categoriaId) {
        this.state.filtros.categoria = categoriaId ? parseInt(categoriaId) : null;
        this.filterAndRenderPrompts();
    },

    handleFilterEtiquetas: function() {
        this.state.filtros.etiquetas = $('#filterEtiquetas').val() || [];
        this.filterAndRenderPrompts();
    },

    toggleFavoritos: function() {
        this.state.filtros.soloFavoritos = !this.state.filtros.soloFavoritos;
        $('#btnMostrarFavoritos').toggleClass('active', this.state.filtros.soloFavoritos);
        this.loadPrompts();
    }
};

// Inicializar cuando el documento esté listo
$(document).ready(function() {
    SmartPromptLibrary.init();
});
