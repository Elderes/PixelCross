using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BikeControll : MonoBehaviour
{
    public Rigidbody2D rodaTraseira;
    public Rigidbody2D rodaDianteira;
    public Rigidbody2D corpoMoto;

    public Animator riderAnimator;

    public SpriteRenderer spriteRendererMotociclista;

    public float forcaTorque = 10f;
    public float forcaGiroAr = 1000f;
    public float velocidadeMaxima = 100f;
    public float aceleracao = 5f;
    public float pesoTranseferencia = 5f;
    private bool rodasNoChao;

    public Camera myCamera;
    public float posicaoInicialX;
    public float posicaoInicialY;
    public float posicaoDistanteX;
    public float posicaoDistanteY;

    public Button leftButton, rightButton;
    private bool isLeftButtonPressed = false;
    private bool isRightButtonPressed = false;

    void Awake()
    {
        InitializeVariables();
    }

    void Start()
    {
        AddMobileListeners();

        if (corpoMoto == null || rodaTraseira == null || rodaDianteira == null || spriteRendererMotociclista == null)
        {
            Debug.LogError("Corpo da moto, rodas ou SpriteRenderer do motociclista não atribuídos no Inspector!");
        }
    }

    private void InitializeVariables()
    {
            // Atribuindo a câmera principal automaticamente
        if (myCamera == null)
        {
            myCamera = Camera.main; // Encontra a câmera principal
        }

        // Atribuindo os botões automaticamente (procurando no Canvas)
        if (leftButton == null)
        {
            leftButton = GameObject.Find("Brake").GetComponent<Button>(); // Encontra o botão pelo nome
        }

        if (rightButton == null)
        {
            rightButton = GameObject.Find("Throttle").GetComponent<Button>(); // Encontra o botão pelo nome
        }
    }

    void Update()
    {
        UpdateCamera();
        VerificarColisaoRodas();
        ControlarAnimacaoMotociclista();

        if (!rodasNoChao)
        {
            ControlarGiroNoAr();
        }

        if (rodasNoChao)
        {
            AcelerarMoto();
            FrearMoto();
        }
    }

    void AddMobileListeners()
    {
        leftButton.GetComponent<EventTrigger>().triggers.Add(CreateEntry(EventTriggerType.PointerDown, () => isLeftButtonPressed = true));
        leftButton.GetComponent<EventTrigger>().triggers.Add(CreateEntry(EventTriggerType.PointerUp, () => isLeftButtonPressed = false));

        rightButton.GetComponent<EventTrigger>().triggers.Add(CreateEntry(EventTriggerType.PointerDown, () => isRightButtonPressed = true));
        rightButton.GetComponent<EventTrigger>().triggers.Add(CreateEntry(EventTriggerType.PointerUp, () => isRightButtonPressed = false));
    }

    private EventTrigger.Entry CreateEntry(EventTriggerType eventID, UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventID;
        entry.callback.AddListener((eventData) => action());
        return entry;
    }

    void VerificarColisaoRodas()
    {
        rodasNoChao = rodaTraseira.IsTouchingLayers(LayerMask.GetMask("Default")) || 
                      rodaDianteira.IsTouchingLayers(LayerMask.GetMask("Default"));

        // Atualiza o parâmetro no Animator
        riderAnimator.SetBool("rodasNoChao", rodasNoChao);
    }

    void ControlarAnimacaoMotociclista()
    {
        float velocidadeAtual = Mathf.Abs(rodaTraseira.angularVelocity);
        
        // Atualiza o parâmetro de velocidade no Animator
        riderAnimator.SetFloat("velocidade", velocidadeAtual);
    }

    void ControlarGiroNoAr()
    {
        float giro = forcaGiroAr * Time.deltaTime;
        float velocidadeAngularMaxima = 500f; // Define a velocidade máxima de rotação (ajuste conforme necessário)

        // Rotação para a direita
        if ((Input.GetKey(KeyCode.RightArrow) || isRightButtonPressed) && corpoMoto.angularVelocity > -velocidadeAngularMaxima)
        {
            corpoMoto.AddTorque(giro);
        }

        // Rotação para a esquerda
        if ((Input.GetKey(KeyCode.LeftArrow) || isLeftButtonPressed) && corpoMoto.angularVelocity < velocidadeAngularMaxima)
        {
            corpoMoto.AddTorque(-giro);
        }
    }


    void AcelerarMoto()
    {
        if (Input.GetKey(KeyCode.RightArrow) || isRightButtonPressed)
        {
            float torqueAtual = Mathf.Clamp(forcaTorque * Time.fixedDeltaTime * aceleracao, 0, forcaTorque);
            float novaVelocidade = Mathf.Clamp(rodaTraseira.angularVelocity - torqueAtual, -velocidadeMaxima, 0);
            rodaTraseira.angularVelocity = novaVelocidade;
            corpoMoto.AddTorque(pesoTranseferencia);
        }
    }

    void FrearMoto()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || isLeftButtonPressed)
        {
            rodaTraseira.angularVelocity = 0f;
            corpoMoto.AddTorque(-pesoTranseferencia);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            rodasNoChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            rodasNoChao = false;
        }
    }

    void UpdateCamera()
    {
        // Define a posição-alvo da câmera quando a moto está no chão
        Vector3 posicaoInicialCamera = new Vector3(corpoMoto.position.x + posicaoInicialX, corpoMoto.position.y + posicaoInicialY, myCamera.transform.position.z);

        // Define uma posição mais distante da câmera quando a moto está no ar
        Vector3 posicaoDistanteCamera = new Vector3(corpoMoto.position.x + posicaoDistanteX, corpoMoto.position.y - posicaoDistanteY, myCamera.transform.position.z);

        // Se a moto estiver no ar, a câmera se afasta suavemente
        if (!rodasNoChao)
        {
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, posicaoDistanteCamera, Time.deltaTime * 0.8f);
            myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, 17f, Time.deltaTime * 2f); // Aumenta o zoom
        }
        else
        {
            // Se a moto estiver no chão, a câmera volta para a posição inicial suavemente
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, posicaoInicialCamera, Time.deltaTime * 2.5f);
            myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, 10f, Time.deltaTime * 1.3f); // Reduz o zoom
        }
    }
}
