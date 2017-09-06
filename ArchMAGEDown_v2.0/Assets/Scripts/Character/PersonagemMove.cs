using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PersonagemMove : MonoBehaviour
{
	[SerializeField] float f_MovingTurnSpeed = 360;//Velocidade final da rotação
	[SerializeField] float f_StationaryTurnSpeed = 180;//Velocidade inicial da Rotação
	[SerializeField] float f_JumpPower = 12f;//Força do impulso do pulo
	[Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;//Slider da velocidade de queda
	//[SerializeField] float f_RunCycleLegOffset = 0.2f;//Correção do tempo de animação
	[SerializeField] float f_MoveSpeedMultiplier = 1f;//Velocidade de movimento
	//[SerializeField] float f_AnimSpeedMultiplier = 1f;//Velocidade da animação
	//[SerializeField] float f_GroundCheckDistance = 0.1f;//Distância do chão

	Rigidbody m_Rigidbody;
	Animator m_Animator;
	bool b_IsGrounded;
	//float f_OrigGroundCheckDistance;
	const float cf_Half = 0.5f;
	float f_TurnAmount;
	float f_ForwardAmount;
	Vector3 v3_GroundNormal;
	//float f_CapsuleHeight;
	//CapsuleCollider m_Capsule;
	//bool m_Crouching;
	//Vector3 v3_CapsuleCenter;

	// Use this for initialization
	void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		//m_Capsule = GetComponent<CapsuleCollider>();
		//f_CapsuleHeight = m_Capsule.height;
		//v3_CapsuleCenter = m_Capsule.center;

		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		//f_OrigGroundCheckDistance = f_GroundCheckDistance;
	}

	// Update is called once per frame
	void Update()
	{

	}

#region RECEBE INTERAÇÃO DO PERSONAGEM  ***enable***
	public void Move(Vector3 move, bool crouch, bool jump, float f_Horizontal)
	{

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		//CheckGroundStatus(); //Se não passar por aqui ele não rotaciona
		v3_GroundNormal = Vector3.up;
		move = Vector3.ProjectOnPlane(move, v3_GroundNormal);
		f_TurnAmount = Mathf.Atan2(move.y, move.z);
		f_ForwardAmount = move.x;

		ApplyExtraTurnRotation();
		m_Rigidbody.AddForce(Vector3.right * f_Horizontal);
		if(move.x != 0)
		{
			m_Animator.SetBool("Andar", true);
			m_Rigidbody.WakeUp();
		}
		else
		{
			m_Animator.SetBool("Andar", false);
			m_Rigidbody.Sleep();
		}


		//Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
		//m_Rigidbody.AddForce(extraGravityForce);

		//HandleGroundedMovement();



		//control and velocity handling is different when grounded and airborne:
		//if (b_IsGrounded)
		//{
		//	HandleGroundedMovement(crouch, jump);
		//}
		//else
		//{
		//	HandleAirborneMovement();
		//}

		//ScaleCapsuleForCrouching(crouch);
		//PreventStandingInLowHeadroom();

		// send input and other state parameters to the animator
		//UpdateAnimator(move);
	}
#endregion

#region AGACHA disable
	void ScaleCapsuleForCrouching(bool crouch)
	{
		//if (b_IsGrounded && crouch)
		//{
		//	if (m_Crouching) return;
		//	m_Capsule.height = m_Capsule.height / 2f;
		//	m_Capsule.center = m_Capsule.center / 2f;
		//	m_Crouching = true;
		//}
		//else
		//{
		//	Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * cf_Half, Vector3.up);
		//	float crouchRayLength = f_CapsuleHeight - m_Capsule.radius * cf_Half;
		//	if (Physics.SphereCast(crouchRay, m_Capsule.radius * cf_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
		//	{
		//		m_Crouching = true;
		//		return;
		//	}
		//	m_Capsule.height = f_CapsuleHeight;
		//	m_Capsule.center = v3_CapsuleCenter;
		//	m_Crouching = false;
		//}
	}
#endregion

#region MANTÉM AGACHADO disable
	//void PreventStandingInLowHeadroom()
	//{
	//	// prevent standing up in crouch-only zones
	//	if (!m_Crouching)
	//	{
	//		Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * cf_Half, Vector3.up);
	//		float crouchRayLength = f_CapsuleHeight - m_Capsule.radius * cf_Half;
	//		if (Physics.SphereCast(crouchRay, m_Capsule.radius * cf_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
	//		{
	//			m_Crouching = true;
	//		}
	//	}
	//}
	#endregion

#region ATUALIZA AS ANIMAÇÕES disable
	void UpdateAnimator(Vector3 move)
	{
		//// update the animator parameters
		//m_Animator.SetFloat("Forward", f_ForwardAmount, 0.1f, Time.deltaTime);
		//m_Animator.SetFloat("Turn", f_TurnAmount, 0.1f, Time.deltaTime);
		//m_Animator.SetBool("Crouch", m_Crouching);
		//m_Animator.SetBool("OnGround", b_IsGrounded);
		//if (!b_IsGrounded)
		//{
		//	m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
		//}

		//// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		//// (This code is reliant on the specific run cycle offset in our animations,
		//// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		//float runCycle =
		//	Mathf.Repeat(
		//		m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + f_RunCycleLegOffset, 1);
		//float jumpLeg = (runCycle < cf_Half ? 1 : -1) * f_ForwardAmount;
		//if (b_IsGrounded)
		//{
		//	m_Animator.SetFloat("JumpLeg", jumpLeg);
		//}

		//// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		//// which affects the movement speed because of the root motion.
		//if (b_IsGrounded && move.magnitude > 0)
		//{
		//	m_Animator.speed = f_AnimSpeedMultiplier;
		//}
		//else
		//{
		//	// don't use that while airborne
		//	m_Animator.speed = 1;
		//}
	}
	#endregion

#region APLICA O PULO disable
	//void HandleAirborneMovement()
	//{
	//	// apply extra gravity from multiplier:
	//	Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
	//	m_Rigidbody.AddForce(extraGravityForce);

	//	f_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? f_OrigGroundCheckDistance : 0.01f;
	//}
	#endregion

#region APLICA O MOVIMENTO ***enable***
	void HandleGroundedMovement()
	{
		//// check whether conditions are right to allow a jump:
		//if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
		//{
		//	// jump!
		//	m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, f_JumpPower, m_Rigidbody.velocity.z);
		//	b_IsGrounded = false;
		//	m_Animator.applyRootMotion = false;
		//	//f_GroundCheckDistance = 0.1f;
		//}
		m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, f_JumpPower, m_Rigidbody.velocity.z);
	}
	#endregion

#region APLICA A ROTAÇÃO  ***enable***
	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(f_StationaryTurnSpeed, f_MovingTurnSpeed, f_ForwardAmount);
		transform.Rotate(0, f_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}
	#endregion

#region ACELERA ANIMAÇÃO disable
	public void OnAnimatorMove()
	{
		//// we implement this function to override the default root motion.
		//// this allows us to modify the positional speed before it's applied.
		//if (b_IsGrounded && Time.deltaTime > 0)
		//{
		//	Vector3 v = (m_Animator.deltaPosition * f_MoveSpeedMultiplier) / Time.deltaTime;

		//	// we preserve the existing y part of the current velocity.
		//	v.y = m_Rigidbody.velocity.y;
		//	m_Rigidbody.velocity = v;
		//}
	}
	#endregion

#region VERIFICA O CHÃO disable
	//void CheckGroundStatus()
	//{
	//	RaycastHit hitInfo;
	//	// helper to visualise the ground check ray in the scene view
	//	Debug.DrawLine(transform.position + (Vector3.up * 0.1f * 10), transform.position + (Vector3.up * 0.1f) + (Vector3.down * f_GroundCheckDistance));
	//	// 0.1f is a small offset to start the ray from inside the character
	//	// it is also good to note that the transform position in the sample assets is at the base of the character
	//	if (Physics.Raycast(transform.position + (Vector3.up * 0.1f * 10), Vector3.down, out hitInfo, f_GroundCheckDistance))
	//	{
	//		v3_GroundNormal = hitInfo.normal;
	//		b_IsGrounded = true;
	//		m_Animator.applyRootMotion = true;
	//	}
	//	else
	//	{
	//		b_IsGrounded = false;
	//		v3_GroundNormal = Vector3.up;
	//		m_Animator.applyRootMotion = false;
	//	}
	//}
	#endregion
}
