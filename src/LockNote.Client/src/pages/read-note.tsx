import { Navigate, useParams } from "react-router-dom";
import { useGetNoteById } from "../api/client";
import { useEffect, useState } from "react";

export const ReadNote = () => {
  const { noteId } = useParams();

  if (!noteId) {
    return <Navigate to="/not-found" replace />;
  }

  // state of password input
  const [password, setPassword] = useState<string | null>(null);

  const { data, refetch, isLoading } = useGetNoteById(noteId, password);

  useEffect(() => {
    refetch();
  }, [noteId]);

  if (data?.id === "passwordIncorrect") {
    return (
      <div>
        <h1>Password Missing</h1>
        <p>The note you are trying to access is password protected.</p>
        <p>Please enter the password to view the note.</p>
        <label className="text-sm font-medium text-gray-400">Password</label>
        <input
          data-testid="password-input"
          type="password"
          className="w-full rounded-lg border border-slate-700 bg-slate-950 p-2 text-sm text-white placeholder-gray-400 focus:border-blue-500 focus:ring-blue-500"
          placeholder="Enter the password"
          onChange={(e) => setPassword(e.target.value)}
        />
        <button
          data-testid="submit-btn"
          type="button"
          className="mt-5 rounded-lg bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-600"
          onClick={() => refetch()}
        >
          Submit
        </button>
      </div>
    );
  }

  return (
    <div>
      <h1>{noteId}</h1>
      {isLoading ? (
        <span>Loading....</span>
      ) : (
        <textarea
          data-testid="message-read"
          id="message"
          title="note content"
          className="outline:ring-purple-700 block w-full rounded-lg border border-slate-700 bg-slate-950 bg-opacity-25 bg-gradient-to-tr from-purple-950/25 via-pink-950/10 to-red-950/10 p-2.5 text-sm text-white placeholder-gray-400 focus:border-purple-700 focus:ring-purple-700"
          readOnly
          disabled
          value={data?.content ?? "Note does not exist"}
        />
      )}
    </div>
  );
};
