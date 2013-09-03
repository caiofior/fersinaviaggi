Imports System.ServiceModel

' NOTA: è possibile utilizzare il comando "Rinomina" del menu di scelta rapida per modificare il nome di interfaccia "Icercavoli_xhr" nel codice e nel file di configurazione contemporaneamente.
<ServiceContract()>
Public Interface ICercavoliXhr

    <OperationContract()>
    Sub DoWork()

End Interface
