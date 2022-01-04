using System;


namespace NiceDishy
{
	
	/// <summary>
	/// Specifies the behaviour for a URI handler.
	/// </summary>
	public interface IUriHandler {

		/// <summary>
		/// Handles the specified URI and returns a value indicating whether any action was taken.
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		bool HandleUri(Uri uri);
	}
}
