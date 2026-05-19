# ¿El fin del programador de Revit? 
## Crónica de experimentos en codificación autónoma

### Script completo de presentación — 90 minutos
**Jeremy Tammik | EUBIM 2026 Valencia**  
**20 de mayo de 2026**

---

## PARTE 0: INTRODUCCIÓN Y BIENVENIDA (5 minutos)

[*Entra al escenario con una sonrisa*]

**Buenos días, buenas dies a todos.** Vaya, qué agradable estar aquí en Valencia. Hace un poco de calor, ¿eh? Pero tranquil, la IA también genera calor... de procesos, claro. 

Mi nombre es Jeremy Tammik. Algunos de ustedes me conocerán como *The Building Coder*. Otros quizá me recuerdan de mis tiempos en Autodesk. Hoy vengo a contarles una crónica—una verdadera historia—de cómo hace poco descubrí que tal vez no necesito tocar el código nunca más.

*Pausa.*

Pero antes de que piensen que soy perezoso, déjenme explicar. Esta es una historia sobre **transición**. De ser programador a ser **orquestador de inteligencia artificial**. Y la buena noticia es que ustedes, los programadores de BIM aquí presentes, están en la mejor posición para experimentar esto ahora mismo.

En los próximos 90 minutos vamos a:
- Repasar la historia: 20 años en Autodesk y el Revit API
- Descubrir el catalizador: un trabajo de MEP que cambió todo
- Explorar la visión: sistemas completamente autónomos
- Ver en vivo cómo funcionan los patrones arquitectónicos
- Reflexionar sobre el futuro

Así que... **¡vamos!** 

---

## PARTE 1: HISTORIA Y CONTEXTO (15 minutos)

### Slide 1: Título

[*Mostrar la diapositiva con el título grande*]

Empecemos con una pregunta: ¿cuántos de ustedes escriben el mismo código una y otra vez? ¿Cuántos responden las mismas preguntas una y otra vez? 

Bueno, eso me pasó durante más de 20 años.

### 2005: Me uno a Autodesk

En 2005, me contrató Autodesk como **technology evangelist**. Mi trabajo era hablar en conferencias, ayudar a desarrolladores, responder preguntas sobre la API de Revit. Era emocionante. La API era nueva, el mundo BIM estaba en expansión.

Escribía código. Explicaba conceptos. Resolvía problemas.

### 2008: Nace The Building Coder

Alrededor de 2008, me preguntaba: "*¿Y si documento todo esto en un blog?*" Así nació **The Building Coder** — una bitácora de proyectos, snippets, soluciones. Pensé que sería útil para algunos colegas.

[*Pausa con una sonrisa*]

Hoy, 18 años después, ese blog tiene casi **3,500 artículos**. No, no estoy bromeando. Trescientos cincuenta mil líneas de documentación de Revit API, explicadas por un tipo que apenas duerme.

### 2010: El foro de discusión

En 2010, lanzamos el **Revit API discussion forum**. Y aquí es donde empieza a ser interesante...

Preguntas. Las mismas preguntas. Una y otra vez. 

"*¿Cómo hago un muro?*"  
"*¿Cómo obtengo el área de una habitación?*"  
"*¿Cómo creo una familia?*"

Quince años respondiendo. Quince años viendo cómo la gente hacía las mismas cosas, de formas ligeramente diferentes, pero fundamentalmente idénticas.

En mi cabeza pensaba: "*Tiene que haber una forma de automatizar esto.*"

### 2017–2024: Q4R4 — El sueño de un motor de búsqueda inteligente

Entonces imaginé: ¿y si construyera un **sistema de respuesta automática** para la API de Revit? Algo que entendiera la pregunta en lenguaje natural y devolviera la solución correcta.

Llamé al proyecto **Q4R4** — *Question Answering for Revit API*.

Exploré:
- **Machine learning** — ¿Puedo entrenar un modelo?
- **Deep learning** — ¿Y redes neuronales?
- **RAG** (Retrieval-Augmented Generation) — ¿Combino búsqueda + generación?
- **ChatGPT** — ¿Y los modelos de lenguaje grande?

Publiqué artículos. Experimenté. Pero siempre fue un proyecto lateral. Mi trabajo de día todavía requería código. Todavía requería respuestas. El sueño quedó... bueno, esperando.

Escribí algunos posts sobre esto:
- "The Revit API Question Answering System Q4R4"
- "Q4R4 Chunking with Claude, using LLM and RAG"
- "ChatGPT for Q4R4"

### June 2025: Jubilación

Después de 20 años en Autodesk, en junio de 2025 decidí que **era hora de parar**. Asistí a mi último evento grande: **DevCon Amsterdam 2025**. Fue emocionante decir adiós al equipo de Autodesk.

[*Muestra foto si está disponible*]

Pensé: "*Bueno, ya está. Fue un buen viaje. Ahora tal vez tenga tiempo para escribir esos libros...*"

No tenía idea de lo que estaba a punto de pasar.

### Resumen de la Parte 1

La clave de esta historia es: **repetición**. Veinte años de repetición. Cuando luego llegue la IA, será porque haremos algo con toda esa repetición.

[Pasa a la siguiente diapositiva]

---

## PARTE 2: EL CATALIZADOR — FEBRERO 2026 (15 minutos)

### Slide: El Catalizador

Así que aquí estoy, jubilado, en Suiza, sin trabajar. Y alguien me llama y dice:

"*Jeremy, ¿podrías ayudarnos a crear un complemento para generar sistemas MEP de HVAC desde datos externos? Por ejemplo, desde un archivo JSON.*"

Ahora bien, aquí vienen dos cosas:

1. **Procrastinación extrema.** No quería empezar. Tenía 1.000 cosas más para hacer. Así que... no hice nada durante un mes.

2. **Sincronización providencial.** Y luego, **justo cuando estaba a punto de atacar el proyecto a finales de enero**, Anthropic anuncia que **Claude Opus 4.6 acaba de salir.**

Opus 4.6 es un modelo **muy poderoso**. Entiende el contexto profundo. Puede escribir código complejo. Y lo mejor: tengo acceso a través de **GitHub Copilot**.

Así que a principios de febrero de 2026, lo intenté.

### El bucle manual

Mi flujo de trabajo fue:

1. **Describo el problema.** "Necesito importar/exportar sistemas HVAC desde JSON a Revit."
2. **Claude genera código C# completo.** Cien líneas, doscientas líneas.
3. **Compilo el código.** Build exitoso.
4. **Lanzo Revit manualmente.** Cliqueo en el botón de mi comando.
5. **Inspeccionó visualmente el modelo.** ¿Funcionó? ¿Está bien?
6. **Si falla:** le digo a Claude exactamente qué salió mal. "*El muro no está alineado.*" "*La puerta está en la posición equivocada.*"
7. **Claude arregla el código.** Vuelvo al paso 3.

Era... increíble. Y también agotador.

### Métricas

En 3-4 días de trabajo intenso:

```
58 + 161 + 74 = 293 requests a Claude
```

Gasté prácticamente todo mi crédito gratuito de febrero en GitHub Copilot. Justo cuando terminé el proyecto.

Pero aquí está lo importante: **Claude escribió TODO el código.** Yo no escribí una sola línea. Mi trabajo fue:
- Describir qué quería
- Lanzar Revit
- Inspeccionar visualmente
- Decirle a Claude qué cambiar

*Pausa reflexiva.*

Y en ese momento pensé dos cosas:

### Insight 1: Tal vez nunca más toque código

"*¿Quién necesita aprender a programar si Claude puede hacer esto?*"

Fue aterrador. También emocionante.

### Insight 2: Pero hay un problema más grande

"*¿Y si pudiera eliminar el paso de 'yo inspeccionando visualmente'?*"

Porque cada inspección manual = pérdida de tiempo. Revit no es como una web app donde puedo abrir las herramientas de desarrollo y ver qué salió mal. En Revit, tengo que:
- Lanzar la aplicación
- Esperar a que cargue (slow)
- Cliquear botones
- Mirar la pantalla
- Decidir si funcionó

¿Y si el **sistema pudiera auto-verificarse**?

### La pregunta clave

[Muestra diapositiva: "La pregunta"]

**¿Puede la IA cerrar el bucle completamente?**

Es decir:

```
Prompt → Código → Compilar → Firmar → Instalar → Lanzar Revit → Ejecutar → Registrar → Verificar
```

**Todo sin intervención humana.**

Esa pregunta se quedó conmigo durante dos meses.

---

## PARTE 3: LA VISIÓN (10 minutos)

### Slide: La Visión

En abril-mayo de 2026, me hice esta pregunta en serio: "*¿Cómo construyo un sistema completamente autónomo?*"

La respuesta no es que necesite una IA más inteligente. Necesito **infraestructura**. Necesito patrones. Necesito que Revit coopere.

### Los cuatro pilares

Hay cuatro cosas que necesito:

#### Pilar 1: Generación de código con IA
Claude, GitHub Copilot, OpenAI Codex — los modelos ahora son lo suficientemente buenos para escribir código Revit API que funcione a la primera (o en pocos intentos).

#### Pilar 2: Firma digital
Aquí está el grande: cuando un complemento sin firma se carga en Revit, aparece un diálogo:
- "Load Always"
- "Load Once"
- "Do Not Load"

Este diálogo **requiere entrada humana**. Un bot no puede responder.

Pero si firmo el complemento con un certificado local, Revit lo confía automáticamente. **Sin diálogo.**

[Pausa.]

Eso es la clave para la automatización.

#### Pilar 3: Eventos de Revit
Revit tiene dos eventos que son críticos:
- `ApplicationInitialized` — disparado cuando Revit está listo
- `Idling` — disparado repetidamente mientras Revit espera entrada

Si suscribo mis comandos a estos eventos, pueden ejecutarse **automáticamente cuando Revit se inicia**, sin que el usuario haga nada.

#### Pilar 4: Registros detallados
Un sistema autónomo no tiene ojos. No puede ver la pantalla. Pero puede escribir archivos de registro.

Cada paso registra sus resultados:
- OnStartup → log
- ApplicationInitialized → log
- Idling → log (¿hay documento activo?)
- Execute → log (¿qué pasó?)

El sistema IA **lee estos logs** y decide si iterar o completar.

### Por qué ahora

¿Por qué es posible ahora?

1. **Los modelos son lo suficientemente buenos.** Claude, Opus, GPT-5 — generan código limpio que funciona.
2. **Los eventos existen desde 2009.** Siempre han estado ahí. Simplemente nunca los usamos así.
3. **La firma local es trivial.** Una línea en el archivo de proyecto.
4. **Logging en .NET es estándar.** File.WriteAllText, TimeSpan.FromSeconds — cosas básicas.

Lo que cambia es la **combinación**. Antes, cada pieza hacía algo pequeño. Juntas, construyen algo nuevo.

[Pausa dramática.]

Un sistema que se escribe a sí mismo.

---

## PARTE 4: TOUR DE ARQUITECTURA — DEMO02 Y CMDLITTLEHOUSE (40 minutos)

### Slide: Componentes del sistema

Ahora vamos a entrar en los detalles técnicos. Quiero mostrarles exactamente cómo funciona.

Tengo un proyecto llamado **Demo02**. Está en la carpeta `Codex/` de mi repositorio. Tiene esta estructura:

```
Codex/Demo02/
├── Application.cs
├── Commands/
│   ├── CmdDemo02.cs
│   └── CmdLittleHouse.cs
├── signing/
│   └── demo02.pfx
├── log/
└── Demo02.csproj
```

Dos comandos. Ambos siguen el mismo patrón. Porque con la IA, **la consistencia es todo**.

### Patrón 1: Separación del comando externo

[Muestra diapositiva de código]

Revit requiere que todos los comandos externos implementen `IExternalCommand`:

```csharp
public class CmdDemo02 : IExternalCommand
{
    public Result Execute(ExternalCommandData data) 
    {
        var doc = data.Application.ActiveUIDocument.Document;
        return Execute2(doc);
    }

    private Result Execute2(Document doc) 
    {
        // El trabajo real
        WriteLog("Hello from Execute2");
        return Result.Succeeded;
    }
}
```

¿Por qué esta separación?

- **Execute:** Es el punto de entrada de Revit. Tiene acceso a ExternalCommandData, que es específico de Revit. **No se puede probar fácilmente.**
- **Execute2:** Es código puro. Toma un Documento. **Se puede probar. Se puede reutilizar. Se puede llamar desde cualquier parte.**

Cuando uso eventos (Idling), no tengo acceso a ExternalCommandData. Pero tengo un Documento. Así que simplemente llamo a **Execute2 directamente**.

La misma lógica. Dos formas de invocación.

**Insight:** Este patrón es simple, pero es lo que permite todo lo demás.

### Patrón 2: Automatización del inicio

[Muestra diapositiva de código]

En Application.cs (el archivo que Revit carga al iniciar):

```csharp
void OnStartup(object s, StartupEventArgs args) 
{
    app.ApplicationInitialized += OnApplicationInitialized;
    WriteLog("OnStartup");
}

void OnApplicationInitialized(object s, EventArgs args) 
{
    app.Idling += OnIdling;
    WriteLog("OnApplicationInitialized");
}

void OnIdling(object s, IdlingEventArgs args) 
{
    var doc = uiApp.ActiveUIDocument?.Document;
    if (doc != null) 
    {
        app.Idling -= OnIdling;  // Desuscribirse
        CmdDemo02.Execute2(doc);  // Ejecutar
    }
}
```

La secuencia:
1. Revit inicia → `OnStartup` se dispara
2. Revit está listo → `ApplicationInitialized` se dispara
3. Revit espera entrada → `Idling` se dispara repetidamente
4. Cuando hay un documento activo → ejecutar el comando

**¿Por qué no directamente en ApplicationInitialized?**

Porque cuando ApplicationInitialized se dispara, **Revit aún no tiene un documento abierto**. Si paso un RVT vía línea de comandos, Revit debe abrirlo primero. Eso toma tiempo. Idling espera a que esté listo.

**Detalle importante:** Me aseguro de mantener la suscripción a Idling hasta que haya un documento. Si me desuscribo demasiado pronto, podría perder el documento cuando se abre.

### Patrón 3: Firma digital

[Muestra diapositiva de código]

Este es el que sorprende a la mayoría de la gente.

En el archivo de proyecto (Demo02.csproj):

```xml
<PropertyGroup>
    <SignAssembly>true</SignAssembly>
    <AssemblyOriginatorKeyFile>signing/demo02.pfx</AssemblyOriginatorKeyFile>
</PropertyGroup>
```

Con esto activado:
1. Genero un certificado local en `signing/demo02.pfx` (una sola vez)
2. Cada compilación **firma automáticamente el DLL** con ese certificado
3. Revit ve el DLL firmado, lo confía (sin diálogo)
4. El complemento se carga silenciosamente

**¿Cómo genero el certificado?**

Es un comando de PowerShell estándar. Pero está fuera del alcance de hoy. La idea importante es: **una línea en tu archivo de proyecto es todo lo que necesitas.**

[Pausa.]

Sin esto, todo lo demás falla. El diálogo de confianza requiere un clic humano.

### Patrón 4: Registro exhaustivo

[Muestra diapositiva de código]

Cada método escribe en un archivo de registro:

```csharp
private static void WriteLog(string methodName, string message) 
{
    string logDir = @"C:\Users\j\w\src\eubim\Codex\Demo02\log";
    string fileName = $"{DateTime.Now:yyyyMMdd-HHmmss-fff}-{methodName}.log";
    string filePath = Path.Combine(logDir, fileName);
    
    File.WriteAllText(filePath, message);
}
```

Cuando se ejecuta, obtengo:
```
20260515-171210-101-OnStartup.log
20260515-171210-101-ApplicationInitialized.log
20260515-171210-101-OnIdling.log
20260515-171210-101-Execute2.log
```

Cuatro archivos. Cuatro pruebas de que el sistema funcionó.

¿Por qué archivos en lugar de una base de datos? **Porque son simples.** Puedo leerlos en cualquier herramienta. Puedo verlos en el Explorador. Son portables.

La IA puede leerlos y decidir: "*OK, Idling se disparó. Execute2 se ejecutó. El sistema funcionó.*"

### El bucle autónomo completo

[Muestra diapositiva de flujo]

Ahora juntemos todo. El **flujo de extremo a extremo**:

1. **Codex genera código** (en mi máquina o en la nube)
2. **Sistema de compilación compila** (msbuild)
3. **Sistema de compilación firma DLL** (con certificado local)
4. **Sistema de compilación instala en Revit** (copia a la carpeta de complementos)
5. **Lanzo Revit desde línea de comandos** con un RVT: `Revit.exe empty.rvt`
6. **Revit inicia, carga complemento firmado** (sin diálogo)
7. **OnStartup → ApplicationInitialized → Idling se disparan**
8. **Comando Execute2 se ejecuta automáticamente**
9. **Archivos de registro se escriben en la carpeta de logs**
10. **Codex lee los logs, verifica el éxito**
11. **Si falla, itera; si tiene éxito, completa**

**Tiempo total: 25-30 segundos.**

Desde "genera código" hasta "verificado y completado" en **menos de un minuto**.

¿Es eso increíble? Sí. ¿Fue fácil construirlo? No. ¿Fue todo IA? La mayor parte, sí.

### Caso de estudio: CmdLittleHouse

[Muestra diapositiva]

Ahora voy a mostrar un ejemplo real. No es un "Hola Mundo". Es un comando real que **modela una casa completa**.

**Especificación:**
- 2 niveles (elevación 0 y 3.000 mm)
- 4 muros formando un rectángulo de 3m × 4m
- 1 piso (pad) en el nivel inferior
- 1 techo inclinado en la parte superior
- 1 puerta centrada en una pared larga
- 3 ventanas en las otras paredes

**¿De dónde vino esta idea?**

De hace 15 años. En los laboratorios de Autodesk, había un proyecto llamado `Lab2_0_CreateLittleHouse`. Es un clásico: un pequeño proyecto de enseñanza que demuestra muros, pisos, techos, familias.

Pensé: "*Si la IA puede construir esta casa autónomamente, entonces puede construir cualquier cosa.*"

### Iteración 1: Geometría básica

[Pausa, como si estuviera mostrando una pantalla]

Codex genera el código. Lanzo Revit. La casa aparece... pero algo está mal.

Las ventanas y puertas no se encuentran. El algoritmo de búsqueda de familias no funciona porque los nombres estándar son diferentes en mi instalación.

**Iteración 1 Fix:** Cambio a búsqueda por categoría, no por nombre:
```csharp
var windows = doc.GetElements(typeof(FamilySymbol))
    .OfType<FamilySymbol>()
    .Where(fs => fs.Family.FamilyCategory.Name == "Windows")
    .First();
```

Codex actualiza el código. Relauncho. Mejor.

### Iteración 2: Refinamiento de geometría

[Pausa]

Ahora las ventanas aparecen, pero están al nivel del suelo. ¿Quién quiere ventanas en el suelo?

También el piso es demasiado pequeño. No alcanza las caras exteriores de los muros.

**Iteración 2 Fix:**
- Ventanas: cambio sill height de 0 a 900 mm
- Piso: expando la huella para que incluya las caras exteriores

Codex itera. Relauncho. Mucho mejor.

### Iteración 3: Techo y unión

[Pausa]

El techo se ve bien, pero **está flotando encima de los muros**. Hay un hueco. En la realidad, el techo debe estar unido a los muros.

Además, el alero (overhang) es demasiado grande. Como 470 mm. Quiero 300 mm.

**Iteración 3 Fix:**
```csharp
roof.Overhang = UnitUtils.ConvertToInternalUnits(0.3, UnitTypeId.Meters);

foreach (var wall in walls) {
    wall.AddAttachment(roof);
}
```

Codex agrega dos cosas: cambiar el valor del alero y agregar la unión.

Relauncho. **Perfecto.** La casa ahora está completa.

### Verificación

Todo registrado:

```
20260516-153000-001-Execute2.log

Created 2 levels at 0 and 3000
Created 4 walls in 3m x 4m rectangle
Created 1 floor (pad)
Created 1 roof
Inserted 1 door at center of long wall
Inserted 3 windows on other walls
All geometry verified. House complete.
```

El archivo de registro dice exactamente qué pasó. Codex lo leyó y dijo: "*Éxito.*"

**¿Eso es lo que es el futuro del desarrollo de Revit?** Podría serlo.

### Resumen de la Parte 4

La arquitectura es simple. Cuatro patrones:
1. Separar Execute de Execute2
2. Usar eventos para iniciar
3. Firmar para confiar automática
4. Registrar todo

Juntos, permiten sistemas completamente autónomos.

---

## PARTE 5: LECCIONES Y FUTURO (15 minutos)

### Slide: Cinco pilares

Ahora reflexionemos. ¿Qué aprendí de todo esto?

#### 1. La IA + estructura = confiabilidad

Los prompts aleatorios generan código mediocre. Pero **cuando le doy a la IA un patrón**, funciona.

Crear un archivo llamado `AGENTS.md` que documenta:
- Cómo se ve Execute vs. Execute2
- Cómo se estructura ApplicationInitialized → Idling
- Dónde van los logs
- Cómo se firma

Cuando Codex lee AGENTS.md antes de generar código, **el código es consistente y confiable**.

Sin AGENTS.md: muchas iteraciones, a menudo fallos.
Con AGENTS.md: pocas iteraciones, casi siempre funciona.

#### 2. La firma lo es todo

Sin firma digital, el sistema se detiene en un diálogo.

Con firma, todo es automático.

Es una lección sobre **gobierno y confianza**. Si quieres automatización, **debes eliminar los pasos manuales de confianza.**

(Nota: Esto tiene implicaciones más amplias. Si la IA genera código malicioso, firmarlo **lo hace más peligroso**, no más seguro. Pero para código de confianza, resolver el diálogo es crítico.)

#### 3. Los eventos son la clave

Revit tiene una arquitectura de **comandos de botón**. El usuario hace clic, el comando se ejecuta.

Pero Revit también tiene **eventos**. Son el puente hacia la automatización.

En el futuro, los desarrolladores de Revit **pensarán más en términos de eventos** que de comandos de botón. O ambos trabajarán juntos.

#### 4. Los logs son visión

Sin logs, un sistema autónomo es un agujero negro. Nunca sabes si funcionó.

**Los logs son los ojos del sistema.**

Cuando la IA ejecuta código en Revit, no puede "verlo" directamente. Pero puede leer archivos. Así que **los archivos de registro son el lenguaje que IA y Revit hablan juntos**.

#### 5. La documentación es fundamental

AGENTS.md no es un lujo. Es una **necesidad operativa**.

Sin ella:
- Codex genera código inconsistente
- Los nuevos desarrolladores no saben qué hacer
- Todo se convierte en un caos

Con ella:
- Todos saben las reglas
- La IA sigue las reglas
- El código es predecible

---

### Slide: Qué sobresale la IA

Algunos dicen: "*Si la IA puede escribir código, ¿qué hacemos los humanos?*"

Buena pregunta. Dejame responder.

**Donde la IA sobresale:**

1. **Boilerplate.** Manifiestos, suscriptores de eventos, getters/setters — la IA los genera automáticamente y correctamente.

2. **Iteración.** Refactorizar, probar, ajustar — la IA puede hacer eso en segundos. Lo que a un humano le toma 2 horas.

3. **Siguiendo patrones.** Si dices: "*Esto debe ser como CmdDemo02*", la IA lo replica perfectamente.

4. **Verificación.** Escribir pruebas, parsear logs, verificar resultados — eso es lo que la IA hace 24/7.

---

### Slide: Donde los humanos todavía importan

Pero aquí está donde la IA **se queda corta**:

1. **Definir el problema.** La IA no entiende qué quieres si no lo dices claramente. Y obtener la especificación correcta requiere **pensamiento humano**, no un LLM.

2. **Crear patrones.** AGENTS.md no se escribió solo. Alguien (en este caso, yo, con ayuda de Codex) tuvo que pensar: "*¿Cuál es la mejor arquitectura?*"

3. **Juzgar trade-offs.** "¿Debería la firma ser local o en la nube? ¿Debería usar Codex o Claude? ¿Debería registrar en archivos o en una base de datos?" Estas son decisiones de **juicio**, no cálculo.

4. **Contexto empresarial.** ¿Por qué estamos construyendo esto? ¿Quién lo usará? ¿Cuál es el presupuesto? La IA no responde estas preguntas.

5. **Responsabilidad.** Si algo sale mal, **alguien debe ser responsable**. Eso es una persona.

---

### Slide: El futuro

Así que ¿cuál es el futuro?

No es: *"Los robots escriben todo, los humanos desaparecen."*

Es: *"Los humanos + la IA colaboran para lograr cosas que ninguno podría solo."*

Especialmente en BIM, donde:
- El dominio es **complejo** (requiere expertos)
- El código es **repetitivo** (perfecto para IA)
- La responsabilidad es **crítica** (requiere juicio humano)

La mejor arquitectura es:
1. El humano **define qué construir** (arquitectura, patrones, especificación)
2. La IA **lo construye y lo prueba** (código, iteración, verificación)
3. El humano **lo revisa y lo libera** (juicio, responsabilidad, gobierno)

---

### Slide: Recomendaciones

Si quieres experimentar con esto:

1. **Documentar tu arquitectura.** Escribe un AGENTS.md para tu proyecto. Haz que sea claro.

2. **Usar un modelo poderoso.** Codex es excelente. Claude Opus también. GPT-5 es excelente. Los modelos baratos a veces fallan.

3. **Empezar pequeño.** No hagas un proyecto de 6 meses. Hace algo que pueda verificarse en una hora.

4. **Logging desde el primer día.** Diseña el logging *mientras* diseñas la arquitectura, no después.

5. **Leer los logs.** Cuando falle, **el log te dirá por qué.** Léelo antes de re-intentar.

---

### Slide: Reflexión final

[Pausa larga.]

Durante 20 años, respondí preguntas sobre Revit API. Preguntas repetidas. Preguntas bien intencionadas.

Pensé que la solución era un motor de búsqueda mejor. O un chatbot.

Pero la verdadera solución resultó ser más simple: **mezclar la IA con infraestructura.**

No es magia. Es ingeniería.

---

## PARTE 6: CIERRE Y PREGUNTAS (5 minutos)

### Slide: Una pregunta diferente

Cuando llegué aquí a Valencia hace 20 años, se preguntaban: "*¿Qué hace la IA por nosotros?*"

Hoy, la pregunta es diferente: "*¿Qué hacemos nosotros cuando la IA hace el trabajo?*"

Y honestamente, no tengo toda la respuesta. Pero tengo algunas ideas:

1. **Nos enfocamos en los problemas, no en el código.** Menos sintaxis, más arquitectura.
2. **Enseñamos máquinas mejor.** Escribimos documentación (AGENTS.md) para que la IA entienda.
3. **Nos volvemos orquestadores.** Orquestamos IA, datos, sistemas, personas.
4. **Dormimos un poco mejor.** Menos bugs que encontrar. Menos boilerplate que escribir.

---

### Slide: Gracias

[Sonríe.]

Gracias a todos ustedes. Está muy bien estar aquí en Valencia, en esta comunidad BIM española. 

Algunos de ustedes serán escépticos. Algunos emocionados. Algunos asustados. Todas las reacciones son válidas.

Pero mi sincero consejo es: **¡pruébalo!** Descarga Codex. Dile a Claude: "*Escribe un comando de Revit.*" Ve qué sale.

Porque la única forma de realmente entender esto es experimentar.

*Pausa final.*

Ahora, preguntas. Les escucho.

---

## NOTAS PARA EL PRESENTADOR

### Timing

- Parte 1 (Historia): 15 min
- Parte 2 (Catalizador): 15 min
- Parte 3 (Visión): 10 min
- Parte 4 (Arquitectura): 40 min ← La parte más importante. Si falta tiempo, acortar aquí solo si es absolutamente necesario, pero mejor mantener todo.
- Parte 5 (Lecciones): 15 min
- Cierre: 5 min
- Q&A: 10 min

**Total: 90 minutos**.

Si tiene que acortar: omitir la Parte 1 un poco (5 min) y las Lecciones pueden ser más rápidas (10 min). Pero la Parte 4 debe ser completa.

### Accesorios/Recursos

- Diapositivas HTML (2026-05-19_eubim_workshop_slides.html)
- Proyecto Demo02 en Codex/ (si es posible, tener listo para ejecutar en vivo)
- Archivos de registro (mostrar al menos uno real)
- AGENTS.md (en Codex/ o en pantalla)
- Foto de DevCon Amsterdam (si está disponible)

### Interacción

- Anime preguntas durante la Parte 4. Si alguien pregunta: "¿Realmente funciona?", diga sí y muestre un log.
- En la Parte 5, invítelos a reflexionar: "¿Qué harían ustedes?". Es una pregunta abierta.
- Al final, déjelos con esperanza, no con miedo. El futuro es colaborativo, no de reemplazo.

### Tono

- **Conversacional.** No es una conferencia académica. Es una crónica, una historia.
- **Honesto.** Admite dificultades, procrastinación, fallas. Eso es más relatable.
- **Emocionado pero realista.** Sí, esto es emocionante. Pero no es ciencia ficción.
- **Levemente irónico.** Un toque de humor vale la pena. "Tal vez nunca más toque código" es una línea que debería sacar una risa nerviosa.

### Valenciano (opcional)

Si siente que puede hacerlo naturalmente, agregue más toques locales:
- "*Vaya, es muy calor aquí en Valencia*" → "*Fa molt de calor! Pues bien, que la IA también genera calor...*"
- "*Tranquil*" es muy valencia
- Mencionar un poco de los valores locales (trabajo en equipo, eficiencia) es agradable

Pero **nunca fuerza el dialecto si no es natural.**

---

## FIN DEL SCRIPT

**Duración esperada: 90 minutos exactamente (incluyendo Q&A)**

¡Buena suerte en Valencia! 🚀

---

*Script preparado por Jeremy Tammik para EUBIM.code() 2026 Valencia*  
*Basado en la crónica de experimentos en codificación autónoma, febrero-mayo 2026*
