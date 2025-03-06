import { useCreateNote } from "../api/client";
import { useQueryClient } from "react-query";
import NoteForm from "../component/note-form";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export const Index = () => {
  const queryClient = useQueryClient();
  const { mutate, data, isError } = useCreateNote(queryClient);
  const navigate = useNavigate();

  useEffect(() => {
    if (data?.id) {
      navigate(`created/${data.id}`, { replace: true });
    }
  }, [data, navigate]);

  return (
    <div className="block">
      <div className="mx-auto mb-4 min-h-8 max-w-2xl">
        <NoteForm mutate={mutate} />
        {isError && <p>An error occurred</p>}
      </div>
      <div className="rounded-lg bg-gray-900 p-8 shadow-lg">
        <h2 className="mb-4 text-2xl font-bold text-white">
          Lock Note Features
        </h2>
        <div className="flex flex-1 flex-wrap gap-4 lg:flex-nowrap">
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">Create Notes</h3>
            <p className="text-gray-600">
              Generate a unique link for secure sharing.
            </p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              One-Time Readability
            </h3>
            <p className="text-gray-600">
              Notes are permanently deleted after being accessed.
            </p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              Password Protection (Optional)
            </h3>
            <p className="text-gray-600">Add an extra layer of security.</p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              Expiration Time
            </h3>
            <p className="text-gray-600">
              Notes automatically expire if not accessed. Your note is
              automatically deleted after a month.
            </p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              End-to-End Encryption
            </h3>
            <p className="text-gray-600">Ensures secure note storage.</p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              Self-Destruct Mechanism
            </h3>
            <p className="text-gray-600">No data is retained after reading.</p>
          </div>
        </div>
      </div>
    </div>
  );
};
