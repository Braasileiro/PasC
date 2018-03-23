using System;

namespace PasC.Models
{
	public enum Tag
	{
		// Keyword
		KW,

		// Operator
		OP_EQ,
		OP_NE,
		OP_GT,
		OP_LT,
		OP_GE,
		OP_LE,
		OP_AD,
		OP_MIN,
		OP_MUL,
		OP_DIV,
		OP_ASS,

		// Symbol
		SMB_OBC,
		SMB_CBC,
		SMB_OPA,
		SMB_CPA,
		SMB_COM,
		SMB_SEM,

		// Comments
		COM_CML,
		COM_ONL,

		// Identifier
		ID,

		// String
		LIT,

		// Constant
		CON_NUM,
		CON_CHAR
	}
}
